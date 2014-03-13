using System.Runtime.InteropServices;

namespace ExpressionDemo.Common
{
    public interface IFilterImplementation
    {
        bool Filter(IGeoDataLocation location);
    }
}