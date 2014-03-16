using System;
using ExpressionDemo.Common;
using ExpressionDemo.ConsoleApp.Applications;
using ExpressionDemo.ConsoleApp.Filters;
using Ninject;
using Ninject.Modules;

namespace ExpressionDemo.ConsoleApp.Ninject
{
    internal class FilterWithGeneratedAssemblyModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IApplication>().To<FilterApplication>();
            Bind<IFilterImplementation>().To(Type.GetType("TestType, TestAssembly"));
            Bind<IFilter>().To<FilterImplementationBridge>();

            Kernel.Load<DataSourceModule>();
        }
    }
}