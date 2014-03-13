using ExpressionDemo.Common;
using ExpressionDemo.ConsoleApp.Configuration;
using ExpressionDemo.ConsoleApp.Filters;
using ExpressionDemo.CsvDataSource;
using Ninject.Modules;

namespace ExpressionDemo.ConsoleApp.Ninject
{
    internal class FilterWithCompiledFilterModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IApplication>().To<FilterApplication>();
            Bind<IGeoDataSource>().To<GeoDataSource>();
            Bind<IFilter>().To<CompiledMethodFilter>();
            Bind<IConfiguration>().To<CapitalCitiesConfiguration>();
        }
    }
}