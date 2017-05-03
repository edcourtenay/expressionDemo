using ExpressionDemo.Common;
using ExpressionDemo.CsvDataSource;
using Ninject.Modules;

namespace ExpressionDemo.ConsoleApp.Ninject
{
    internal class DataSourceModule : NinjectModule {
        public override void Load()
        {
            //Bind<IGeoDataSource>().To<EmbeddedGeoDataSource>();
            Bind<IGeoDataSource>().To<FileGeoDataSource>().WithConstructorArgument("fileName", @"allCountries.txt");
        }
    }
}