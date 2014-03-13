using System;
using System.Collections.Generic;

namespace ExpressionDemo.ConsoleApp.Configuration
{
    public class EuropeanConfiguration : BaseConfiguration
    {
        public override IEnumerable<string> CountryCodes
        {
            get { return new[] { "gb", "fr", "de" }; }
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
