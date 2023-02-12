using System;
using System.Collections.Generic;
using System.IO;
using ExpressionDemo.Common;

namespace ExpressionDemo.CsvDataSource
{
    public class FileGeoDataSource : GeoDataSourceBase, IGeoDataSource
    {
        private readonly string _fileName;

        public FileGeoDataSource(string fileName)
        {
            _fileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
        }

        public IEnumerable<IGeoDataLocation> Locations()
        {
            using (Stream stream = File.OpenRead(_fileName))
                foreach (var geoDataLocation in ReadLocationsFromStream(stream))
                    yield return geoDataLocation;
        }
    }
}