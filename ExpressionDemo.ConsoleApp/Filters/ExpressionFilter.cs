using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using ExpressionDemo.Common;
using ExpressionDemo.ConsoleApp.Extensions;

namespace ExpressionDemo.ConsoleApp.Filters
{
    public class ExpressionFilter : IFilter
    {
        private readonly IConfiguration _configuration;

        public ExpressionFilter(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException("configuration");

            _configuration = configuration;
        }

        public Func<IGeoDataLocation, bool> GetFilterFunction()
        {
            return GetFilterExpression().Compile();
        }

        public Expression<Func<IGeoDataLocation, bool>> GetFilterExpression()
        {
            return BuildExpression();
        }

        private Expression<Func<IGeoDataLocation, bool>> BuildExpression()
        {
            ParameterExpression location = Expression.Variable(typeof (IGeoDataLocation), "location");

            return Expression.Lambda<Func<IGeoDataLocation, bool>>(ExpressionSections(location).AndAlso(),
                location);
        }

        private IEnumerable<Expression> ExpressionSections(ParameterExpression location)
        {
            if (_configuration.CountryCodes.Any())
                yield return CountryCodesExpression(location);

            if (_configuration.Features.Any())
                yield return FeaturesExpression(location);

            if (_configuration.MinimumPopulationSize.HasValue)
                yield return PopulationExpression(location);

            if (_configuration.ModifiedAfter.HasValue)
                yield return ModifiedDateExpression(location);
        }

        private Expression CountryCodesExpression(ParameterExpression location)
        {
            return _configuration.CountryCodes
                .Select(code => CountryCodeExpression(location, code))
                .OrElse();
        }

        private Expression CountryCodeExpression(ParameterExpression location, string code)
        {
            return StringEqualsExpression(Expression.Property(location, "CountryCode"), Expression.Constant(code));
        }

        private Expression FeaturesExpression(ParameterExpression location)
        {
            return _configuration.Features
                .Select(tuple => FeatureExpression(location, tuple))
                .OrElse();
        }

        private Expression FeatureExpression(ParameterExpression location, Tuple<string, string> tuple)
        {
            return new[] {
                    StringEqualsExpression(Expression.Property(location, "FeatureClass"),
                        Expression.Constant(tuple.Item1)),
                    StringEqualsExpression(Expression.Property(location, "FeatureCode"),
                        Expression.Constant(tuple.Item2))
            }.AndAlso();
        }

        private Expression PopulationExpression(ParameterExpression location)
        {
            return Expression.GreaterThanOrEqual(Expression.Property(location, "Population"),
                Expression.Constant(_configuration.MinimumPopulationSize ?? 0));
        }

        private Expression ModifiedDateExpression(ParameterExpression location)
        {
            Debug.Assert(_configuration.ModifiedAfter != null, "_configuration.ModifiedAfter != null");

            var ticks = _configuration.ModifiedAfter.Value.Ticks;

            return Expression.GreaterThanOrEqual(Expression.Property(Expression.Property(location, "ModificationDate"), "Ticks"),
                Expression.Constant(ticks));
        }

        private Expression StringEqualsExpression(Expression expr1, Expression expr2)
        {
            return Expression.Call(typeof (string).GetMethod("Equals",
                new[] { typeof (string), typeof (string), typeof (StringComparison) }), expr1, expr2,
                Expression.Constant(StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
