using ExpressionDemo.Common;
using ExpressionDemo.ConsoleApp.Applications;
using ExpressionDemo.ConsoleApp.Configurations;
using Ninject.Modules;

namespace ExpressionDemo.ConsoleApp.Ninject
{
    internal class AssemblyGeneratorModule : NinjectModule {
        public override void Load()
        {
            Bind<IApplication>().To<AssemblyGeneratorApplication>();
            Bind<IConfiguration>().To<EuropeanConfiguration>();
        }
    }
}