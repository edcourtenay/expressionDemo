using System;
using ExpressionDemo.Common;
using ExpressionDemo.ConsoleApp.Filters;
using ExpressionDemo.CsvDataSource;
using Ninject.Modules;

namespace ExpressionDemo.ConsoleApp.Ninject
{
    internal class FilterWithGeneratedAssemblyModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IApplication>().To<FilterApplication>();
            Bind<IGeoDataSource>().To<GeoDataSource>();
            Bind<IFilterImplementation>().To(Type.GetType("TestType, TestAssembly"));
            Bind<IFilter>().To<FilterImplementationBridge>();
        }
    }
}