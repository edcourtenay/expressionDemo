using System;
using System.Collections.Generic;
using ExpressionDemo.Common;

namespace ExpressionDemo.CsvDataSource
{
    public class GeoDataLocation : IGeoDataLocation
    {
        public int GeoNameId { get; set; }
        public string Name { get; set; }
        public string PlainTextName { get; set; }
        public IEnumerable<string> AlternateNames { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string FeatureClass { get; set; }
        public string FeatureCode { get; set; }
        public string CountryCode { get; set; }
        public string AlternateCountryCode { get; set; }
        public string AdminCode1 { get; set; }
        public string AdminCode2 { get; set; }
        public string AdminCode3 { get; set; }
        public string AdminCode4 { get; set; }
        public long Population { get; set; }
        public long Elevation { get; set; }
        public string DigitalElevationModel { get; set; }
        public string TimeZone { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}