using System.Collections.Generic;
using System.Reflection;
using ExpressionDemo.Common;

namespace ExpressionDemo.CsvDataSource
{
    public class EmbeddedGeoDataSource : GeoDataSourceBase, IGeoDataSource
    {
        public IEnumerable<IGeoDataLocation> Locations()
        {
            var assembly = Assembly.GetExecutingAssembly();
            const string resourceName = "ExpressionDemo.CsvDataSource.Embedded.cities15000.txt";

            using (var stream = assembly.GetManifestResourceStream(resourceName))
                foreach (var geoDataLocation in ReadLocationsFromStream(stream))
                    yield return geoDataLocation;
        }
    }
}
