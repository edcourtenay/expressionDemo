using System;
using System.Collections.Generic;

namespace ExpressionDemo.ConsoleApp.Configuration
{
    public class CapitalCitiesConfiguration : BaseConfiguration
    {
        public override IEnumerable<Tuple<string, string>> Features
        {
            get
            {
                return new[]
                {
                    new Tuple<string, string>("p", "pplc")
                };
            }
        }

        //public override IEnumerable<string> CountryCodes
        //{
        //    get { return new[] { "gb", "fr", "de" }; }
        //}
    }
}