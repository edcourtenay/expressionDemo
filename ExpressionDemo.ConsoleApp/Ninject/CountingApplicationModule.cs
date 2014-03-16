using System.Runtime.Remoting.Messaging;
using ExpressionDemo.Common;
using ExpressionDemo.ConsoleApp.Applications;
using ExpressionDemo.ConsoleApp.Configurations;
using ExpressionDemo.ConsoleApp.Filters;
using Ninject;
using Ninject.Modules;

namespace ExpressionDemo.ConsoleApp.Ninject
{
    internal class CountingApplicationModule : NinjectModule {
        public override void Load()
        {
            Bind<IApplication>().To<CountingApplication>();

            Bind<IConfiguration>().To<EuropeanConfiguration>();

            Bind<IFilter>().To<SimpleFilter>();
            Bind<IFilter>().To<FuncFilter>();
            Bind<IFilter>().To<ExpressionFuncFilter>();
            Bind<IFilter>().To<ExpressionFilter>();
            //Bind<IFilter>()
            //    .To<FilterImplementationBridge>()
            //    .WithConstructorArgument("filterImplementation", Activator.CreateInstance(Type.GetType("TestType, TestAssembly")));

            Kernel.Load<DataSourceModule>();
        }
    }
}