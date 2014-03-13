using System;
using System.Collections.Generic;
using System.Linq;
using ExpressionDemo.Common;

namespace ExpressionDemo.ConsoleApp.Filters
{
    public class FuncFilter : IFilter
    {
        private readonly IConfiguration _configuration;
        private readonly Func<IGeoDataLocation, bool>[] _funcs;

        public FuncFilter(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException("configuration");

            _configuration = configuration;
            _funcs = new[] { CountryCodesExpression(),
                FeaturesExpression(),
                PopulationExpression(),
                ModifiedDateExpression()};
        }

        public Func<IGeoDataLocation, bool> GetFilterFunction()
        {
            return location => _funcs.All(func => func(location));
        }

        private Func<IGeoDataLocation, bool> CountryCodesExpression()
        {
            IEnumerable<string> countryCodes = _configuration.CountryCodes;

            string[] enumerable = countryCodes as string[] ?? countryCodes.ToArray();

            if (enumerable.Any())
                return location => enumerable
                    .Any(s => location.CountryCode.Equals(s, StringComparison.InvariantCultureIgnoreCase));

            return location => true;
        }

        private Func<IGeoDataLocation, bool> FeaturesExpression()
        {
            return location => _configuration.Features
                .Select(FeatureExpression)
                .Any(func => func(location));
        }

        private static Func<IGeoDataLocation, bool> FeatureExpression(Tuple<string, string> tuple)
        {
            return location =>
                (string.Equals(tuple.Item1, location.FeatureClass,
                    StringComparison.InvariantCultureIgnoreCase) &&
                    string.Equals(tuple.Item2, location.FeatureCode,
                        StringComparison.InvariantCultureIgnoreCase));
        }

        private Func<IGeoDataLocation, bool> PopulationExpression()
        {
            if (_configuration.MinimumPopulationSize.HasValue)
                return location => location.Population >= _configuration.MinimumPopulationSize;

            return location => true;
        }

        private Func<IGeoDataLocation, bool> ModifiedDateExpression()
        {
            if (_configuration.ModifiedAfter.HasValue)
                return location => location.ModificationDate >= _configuration.ModifiedAfter.Value;

            return location => true;
        }
    }
}