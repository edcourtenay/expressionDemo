using ExpressionDemo.Common;
using ExpressionDemo.ConsoleApp.Configurations;
using Ninject.Modules;

namespace ExpressionDemo.ConsoleApp.Ninject
{
    internal class ConfigurationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IConfiguration>().To<EuropeanConfiguration>();
            //Bind<IConfiguration>().To<CapitalCitiesConfiguration>();
        }
    }
}