using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ExpressionDemo.Common;

namespace ExpressionDemo.ConsoleApp.Applications
{
    internal class CountingApplication : IApplication
    {
        private readonly IEnumerable<IFilter> _filters;
        private readonly IGeoDataSource _dataSource;

        public CountingApplication(IGeoDataSource dataSource, IEnumerable<IFilter> filters)
        {
            if (dataSource == null)
                throw new ArgumentNullException("dataSource");
            if (filters == null)
                throw new ArgumentNullException("filters");

            _dataSource = dataSource;
            _filters = filters;
        }

        public void Run()
        {
            var filters = _filters.Select(filter => new {
                filter.GetType().Name,
                Filter = filter.GetFilterFunction()
            });

            foreach (var filter in filters)
                TimeOperation(filter.Name, filter.Filter);
        }

        public void TimeOperation(string name, Func<IGeoDataLocation, bool> filterFunction)
        {
            Console.WriteLine(name);
            Console.WriteLine(new string('-', name.Length));

            GC.Collect();
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            
            int count = _dataSource.Locations()
                .Count(filterFunction);
            
            stopwatch.Stop();

            Console.WriteLine("{0:N0} matching records", count);
            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
            Console.WriteLine();
        }
    }
}
