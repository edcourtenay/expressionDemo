using System;
using System.Collections.Generic;

namespace ExpressionDemo.ConsoleApp.Configuration
{
    public class EuropeanConfiguration : BaseConfiguration
    {
        public override IEnumerable<string> CountryCodes
        {
            get
            {
                return new[]
                {
                    "al", "ad", "at", "by", "be", "ba", "bg", "hr", "cy",
                    "cz", "dk", "ee", "fo", "fi", "fr", "de", "gi", "gr",
                    "hu", "is", "ie", "it", "lv", "li", "lt", "lu", "mk",
                    "mt", "md", "mc", "nl", "no", "pl", "pt", "ro", "ru",
                    "sm", "rs", "sk", "si", "es", "se", "ch", "ua", "gb",
                    "va", "im", "me"
                };
            }
        }

        public override IEnumerable<Tuple<string, string>> Features
        {
            get { return new[] {
                new Tuple<string, string>("p", "ppl")
            }; }
        }

        public override long? MinimumPopulationSize
        {
            get { return 100000L; }
        }

        public override DateTime? ModifiedAfter
        {
            get { return new DateTime(2012, 1, 1); }
        }
    }


}
