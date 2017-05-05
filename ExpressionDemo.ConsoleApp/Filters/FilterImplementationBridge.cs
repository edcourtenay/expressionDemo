using System;
using ExpressionDemo.Common;

namespace ExpressionDemo.ConsoleApp.Filters
{
    internal class FilterImplementationBridge : IFilter
    {
        private readonly IFilterImplementation _filterImplementation;

        public FilterImplementationBridge(IFilterImplementation filterImplementation)
        {
            _filterImplementation = filterImplementation ?? throw new ArgumentNullException(nameof(filterImplementation));
        }

        public Func<IGeoDataLocation, bool> GetFilterFunction()
        {
            return _filterImplementation.Filter;
        }
    }
}