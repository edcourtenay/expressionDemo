using System;

namespace ExpressionDemo.Common
{
    public interface IFilter
    {
        Func<IGeoDataLocation, bool> GetFilterFunction();
    }
}