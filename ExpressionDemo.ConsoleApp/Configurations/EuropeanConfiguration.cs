using System;
using System.Collections.Generic;

namespace ExpressionDemo.ConsoleApp.Configurations
{
    public class EuropeanConfiguration : BaseConfiguration
    {
        public override IEnumerable<string> CountryCodes => new[]
        {
            "al", "ad", "at", "by", "be", "ba", "bg", "hr", "cy",
            "cz", "dk", "ee", "fo", "fi", "fr", "de", "gi", "gr",
            "hu", "is", "ie", "it", "lv", "li", "lt", "lu", "mk",
            "mt", "md", "mc", "nl", "no", "pl", "pt", "ro", "ru",
            "sm", "rs", "sk", "si", "es", "se", "ch", "ua", "gb",
            "va", "im", "me"
        };

        public override IEnumerable<Tuple<string, string>> Features => new[] {
            // a city, town, village, or other agglomeration of buildings where people live and work
            new Tuple<string, string>("p", "ppl"),
            // capital of a political entity
            new Tuple<string, string>("p", "pplc") 
        };

        public override long? MinimumPopulationSize => 100000L;

        public override DateTime? ModifiedAfter => new DateTime(2012, 1, 1);
    }
}
