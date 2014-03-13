using System;
using System.Collections.Generic;
using System.Linq;
using ExpressionDemo.Common;

namespace ExpressionDemo.ConsoleApp.Configuration
{
    public abstract class BaseConfiguration : IConfiguration
    {
        public virtual IEnumerable<string> CountryCodes
        {
            get { return Enumerable.Empty<string>(); }
        }

        public virtual IEnumerable<Tuple<string, string>> Features
        {
            get { return Enumerable.Empty<Tuple<string, string>>(); }
        }

        public virtual long? MinimumPopulationSize
        {
            get { return null; }
        }

        public virtual DateTime? ModifiedAfter
        {
            get { return null; }
        }
    }
}