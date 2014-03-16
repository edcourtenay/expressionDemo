using System.Collections.Generic;
using System.IO;
using ExpressionDemo.Common;

namespace ExpressionDemo.CsvDataSource
{
    public abstract class GeoDataSourceBase {
        protected readonly CsvDataSourceParser _csvDataSourceParser = new CsvDataSourceParser();

        public IEnumerable<IGeoDataLocation> ReadLocationsFromStream(Stream stream)
        {
            using (var reader = new StreamReader(stream)) {
                string line;
                while ((line = reader.ReadLine()) != null)
                    yield return _csvDataSourceParser.ParseLine(line);
            }
        }
    }
}