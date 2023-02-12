using System;
using System.Linq;
using ExpressionDemo.Common;
using LinqKit;

namespace ExpressionDemo.ConsoleApp.Applications
{
    internal class FilterApplication : IApplication
    {
        private readonly IGeoDataSource _dataSource;
        private readonly IFilter _filter;

        public FilterApplication(IGeoDataSource dataSource, IFilter filter)
        {
            _dataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
            _filter = filter ?? throw new ArgumentNullException(nameof(filter));
        }

        public void Run()
        {
            var filterFunction = _filter.GetFilterFunction();
            _dataSource.Locations()
                .Where(filterFunction)
                .ForEach(location => Console.WriteLine("{1}: {0}", location.Name, location.CountryCode) );
        }
    }
}