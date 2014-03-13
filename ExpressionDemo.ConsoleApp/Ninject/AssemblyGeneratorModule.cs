using ExpressionDemo.Common;
using ExpressionDemo.ConsoleApp.Configuration;
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