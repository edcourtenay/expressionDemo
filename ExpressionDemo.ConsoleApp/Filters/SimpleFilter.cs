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
            if (configuration == null)
                throw new ArgumentNullException("configuration");

            _configuration = configuration;
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
            
            foreach (string countryCode in _configuration.CountryCodes) {
                if (location.CountryCode.Equals(countryCode, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }

        private bool FilterByClassification(IGeoDataLocation location)
        {
            foreach (var feature in _configuration.Features) {
                if (string.Equals(feature.Item1, location.FeatureClass, StringComparison.InvariantCultureIgnoreCase) &&
                    string.Equals(feature.Item2, location.FeatureCode, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
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
