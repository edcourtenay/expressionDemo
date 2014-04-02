using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ExpressionDemo.Common;

namespace ExpressionDemo.CsvDataSource
{
    public class EmbeddedGeoDataSource : GeoDataSourceBase, IGeoDataSource
    {
        public IEnumerable<IGeoDataLocation> Locations()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            const string resourceName = "ExpressionDemo.CsvDataSource.Embedded.cities15000.txt";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            foreach (var geoDataLocation in ReadLocationsFromStream(stream))
                yield return geoDataLocation;
        }
    }
}
