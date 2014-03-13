using System;
using System.Collections.Generic;

namespace ExpressionDemo.Common
{
    public interface IGeoDataLocation
    {
        int GeoNameId { get; }
        string Name { get; }
        string PlainTextName { get; }
        IEnumerable<string> AlternateNames { get; }
        decimal Latitude { get; }
        decimal Longitude { get; }
        string FeatureClass { get; }
        string FeatureCode { get; }
        string CountryCode { get; }
        string AlternateCountryCode { get; }
        string AdminCode1 { get; }
        string AdminCode2 { get; }
        string AdminCode3 { get; }
        string AdminCode4 { get; }
        long Population { get; }
        long Elevation { get; }
        string DigitalElevationModel { get; }
        string TimeZone { get; }
        DateTime ModificationDate { get; }
    }
}