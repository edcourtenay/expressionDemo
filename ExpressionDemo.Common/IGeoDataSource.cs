using System.Collections.Generic;

namespace ExpressionDemo.Common
{
    public interface IGeoDataSource
    {
        IEnumerable<IGeoDataLocation> Locations();
    }
}
