using System;
using System.Linq;
using ExpressionDemo.Common;

namespace ExpressionDemo.ConsoleApp.Filters
{
    public class SimpleFilter : IFilter
    {
        private readonly IConfiguration _configuration;

        public SimpleFilter(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public Func<IGeoDataLocation, bool> GetFilterFunction()
        {
            return location => FilterByCountryCode(location) && FilterByClassification(location)
                && FilterByPopulation(location) && FilterByModificationDate(location);
        }

        private bool FilterByCountryCode(IGeoDataLocation location)
        {
            if (!_configuration.CountryCodes.Any())
                return true;

            return _configuration.CountryCodes
                .Any(countryCode => location.CountryCode.Equals(countryCode, StringComparison.InvariantCultureIgnoreCase));
        }

        private bool FilterByClassification(IGeoDataLocation location)
        {
            return _configuration.Features
                .Any(feature => string.Equals(feature.Item1, location.FeatureClass, StringComparison.InvariantCultureIgnoreCase) && string.Equals(feature.Item2, location.FeatureCode, StringComparison.InvariantCultureIgnoreCase));
        }

        private bool FilterByPopulation(IGeoDataLocation location)
        {
            if (_configuration.MinimumPopulationSize.HasValue)
                return location.Population >= _configuration.MinimumPopulationSize.Value;

            return true;
        }

        private bool FilterByModificationDate(IGeoDataLocation location)
        {
            if (_configuration.ModifiedAfter.HasValue)
                return location.ModificationDate >= _configuration.ModifiedAfter;

            return true;
        }
    }
}
