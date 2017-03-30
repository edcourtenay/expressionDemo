using System;
using System.Linq;
using System.Linq.Expressions;
using ExpressionDemo.Common;
using ExpressionDemo.ConsoleApp.Extensions;
using LinqKit;

namespace ExpressionDemo.ConsoleApp.Filters
{
    public class ExpressionFuncFilter : IFilter
    {
        private readonly IConfiguration _configuration;

        public ExpressionFuncFilter(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            _configuration = configuration;
        }

        public Func<IGeoDataLocation, bool> GetFilterFunction()
        {
            return GetFilterExpression().Compile();
        }

        public Expression<Func<IGeoDataLocation, bool>> GetFilterExpression()
        {
            return CountryCodesExpression()
                .And(FeaturesExpression())
                .And(PopulationExpression())
                .And(ModifiedDateExpression());
        }

        private Expression<Func<IGeoDataLocation, bool>> CountryCodesExpression()
        {
            var countryCodes = _configuration.CountryCodes;

            var enumerable = countryCodes as string[] ?? countryCodes.ToArray();
            if (enumerable.Any())
                return enumerable.Select(CountryCodeExpression).OrElse();

            return location => true;
        }

        private static Expression<Func<IGeoDataLocation, bool>> CountryCodeExpression(string code)
        {
            return location => location.CountryCode.Equals(code, StringComparison.InvariantCultureIgnoreCase);
        }

        private Expression<Func<IGeoDataLocation, bool>> FeaturesExpression()
        {
            return _configuration.Features
                .Select(FeatureExpression).OrElse();
        }

        private static Expression<Func<IGeoDataLocation, bool>> FeatureExpression(Tuple<string, string> tuple)
        {
            return location =>
                (string.Equals(tuple.Item1, location.FeatureClass,
                    StringComparison.InvariantCultureIgnoreCase) &&
                    string.Equals(tuple.Item2, location.FeatureCode,
                        StringComparison.InvariantCultureIgnoreCase));
        }

        private Expression<Func<IGeoDataLocation, bool>> PopulationExpression()
        {
            if (_configuration.MinimumPopulationSize.HasValue)
                return location => location.Population >= _configuration.MinimumPopulationSize;

            return location => true;
        }

        private Expression<Func<IGeoDataLocation, bool>> ModifiedDateExpression()
        {
            if (_configuration.ModifiedAfter.HasValue)
                return location => location.ModificationDate >= _configuration.ModifiedAfter.Value;

            return location => true;
        }
    }
}
