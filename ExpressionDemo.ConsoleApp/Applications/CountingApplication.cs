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
            _dataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
            _filters = filters ?? throw new ArgumentNullException(nameof(filters));
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
            
            var count = _dataSource.Locations()
                .AsParallel()
                .Where(filterFunction)
                .Count();
            
            stopwatch.Stop();

            Console.WriteLine("{0:N0} matching records", count);
            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
            Console.WriteLine();
        }
    }
}
