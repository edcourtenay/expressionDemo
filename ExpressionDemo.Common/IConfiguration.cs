using System;
using System.Collections.Generic;

namespace ExpressionDemo.Common
{
    public interface IConfiguration
    {
        IEnumerable<string> CountryCodes { get; }
        IEnumerable<Tuple<string, string>> Features { get; }
        long? MinimumPopulationSize { get; }
        DateTime? ModifiedAfter { get; }
    }
}