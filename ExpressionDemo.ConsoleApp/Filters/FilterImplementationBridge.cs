using System;
using ExpressionDemo.Common;

namespace ExpressionDemo.ConsoleApp.Filters
{
    internal class FilterImplementationBridge : IFilter
    {
        private readonly IFilterImplementation _filterImplementation;

        public FilterImplementationBridge(IFilterImplementation filterImplementation)
        {
            if (filterImplementation == null) throw new ArgumentNullException("filterImplementation");

            _filterImplementation = filterImplementation;
        }

        public Func<IGeoDataLocation, bool> GetFilterFunction()
        {
            return _filterImplementation.Filter;
        }
    }
}