using System;
using System.Collections.Generic;

namespace ExpressionDemo.ConsoleApp.Configurations
{
    public class CapitalCitiesConfiguration : BaseConfiguration
    {
        public override IEnumerable<Tuple<string, string>> Features => new[]
        {
            new Tuple<string, string>("p", "pplc")
        };

        public override IEnumerable<string> CountryCodes => new[]
            { "gb", "fr", "de" };
    }
}