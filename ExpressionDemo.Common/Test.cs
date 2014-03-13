namespace ExpressionDemo.Common
{
    public class Test : IFilterImplementation
    {
        public static bool FilterStatic(IGeoDataLocation location)
        {
            return true;
        }

        public bool Filter(IGeoDataLocation location)
        {
            return FilterStatic(location);
        }
    }
}