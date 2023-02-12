using System;
using System.Collections.Generic;
using System.Linq;
using ExpressionDemo.Common;

namespace ExpressionDemo.ConsoleApp.Configurations
{
    public abstract class BaseConfiguration : IConfiguration
    {
        public virtual IEnumerable<string> CountryCodes => Enumerable.Empty<string>();

        public virtual IEnumerable<Tuple<string, string>> Features => Enumerable.Empty<Tuple<string, string>>();

        public virtual long? MinimumPopulationSize => null;

        public virtual DateTime? ModifiedAfter => null;
    }
}