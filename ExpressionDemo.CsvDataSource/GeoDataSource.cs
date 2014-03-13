using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using ExpressionDemo.Common;

namespace ExpressionDemo.CsvDataSource
{
    public class GeoDataSource : IGeoDataSource
    {
        public IEnumerable<IGeoDataLocation> Locations()
        {
            using (Stream stream = new FileStream(@"C:\Data\allCountries\allCountries.txt", FileMode.Open))
            using (var reader = new StreamReader(stream)) {
                string line;
                while ((line = reader.ReadLine()) != null)
                    yield return ParseLine(line);
            }
        }

        private IGeoDataLocation ParseLine(string line)
        {
            string[] columns = line.Split('\t');

            return new GeoDataLocation {
                GeoNameId = Int32.Parse(columns[0]),
                Name = columns[1],
                PlainTextName = columns[2],
                AlternateNames = columns[3].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries),
                Latitude = Decimal.Parse(columns[4]),
                Longitude = Decimal.Parse(columns[5]),
                FeatureClass = columns[6],
                FeatureCode = columns[7],
                CountryCode = columns[8],
                AlternateCountryCode = columns[9],
                AdminCode1 = columns[10],
                AdminCode2 = columns[11],
                AdminCode3 = columns[12],
                AdminCode4 = columns[13],
                Population = Int64.Parse(columns[14]),
                //Elevation = Int64.Parse(columns[15]),
                DigitalElevationModel = columns[16],
                TimeZone = columns[17],
                ModificationDate = DateTime.ParseExact(columns[18], "yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo)
            };
        }
    }
}
