using System;
using ExpressionDemo.Common;
using ExpressionDemo.ConsoleApp.Configuration;
using ExpressionDemo.ConsoleApp.Filters;
using ExpressionDemo.CsvDataSource;
using Ninject.Modules;

namespace ExpressionDemo.ConsoleApp.Ninject
{
    internal class CountingApplicationModule : NinjectModule {
        public override void Load()
        {
            Bind<IApplication>().To<CountingApplication>();

            Bind<IGeoDataSource>().To<GeoDataSource>();
            Bind<IConfiguration>().To<EuropeanConfiguration>();

            Bind<IFilter>().To<StandardFilter>();
            //Bind<IFilter>().To<FuncFilter>();
            //Bind<IFilter>().To<ExpressionFuncFilter>();
            //Bind<IFilter>().To<ExpressionFilter>();
            //Bind<IFilter>()
            //    .To<FilterImplementationBridge>()
            //    .WithConstructorArgument("filterImplementation", Activator.CreateInstance(Type.GetType("TestType, TestAssembly")));
        }
    }
}