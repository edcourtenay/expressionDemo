using ExpressionDemo.Common;
using ExpressionDemo.ConsoleApp.Applications;
using Ninject.Modules;

namespace ExpressionDemo.ConsoleApp.Ninject
{
    internal class RpnCalculatorModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IApplication>().To<RpnCalculatorApplication>();
        }
    }
}