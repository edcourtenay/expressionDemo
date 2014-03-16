using System;
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
            string resourceName = "ExpressionDemo.CsvDataSource.Embedded.cities15000.txt";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            foreach (var geoDataLocation in ReadLocationsFromStream(stream))
                yield return geoDataLocation;
        }
    }

    public class FileGeoDataSource : GeoDataSourceBase, IGeoDataSource
    {
        private readonly string _fileName;

        public FileGeoDataSource(string fileName)
        {
            if (fileName == null)
                throw new ArgumentNullException("fileName");

            _fileName = fileName;
        }

        public IEnumerable<IGeoDataLocation> Locations()
        {
            using (Stream stream = File.OpenRead(_fileName))
                foreach (var geoDataLocation in ReadLocationsFromStream(stream))
                    yield return geoDataLocation;
        }
    }
}
