using ExpressionDemo.Common;
using ExpressionDemo.ConsoleApp.Applications;
using ExpressionDemo.ConsoleApp.Configurations;
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
            Bind<IFilter>().To<ExpressionFilter>();
            Bind<IConfiguration>().To<CapitalCitiesConfiguration>();
        }
    }
}