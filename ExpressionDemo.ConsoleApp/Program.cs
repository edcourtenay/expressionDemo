using ExpressionDemo.Common;
using ExpressionDemo.ConsoleApp.Ninject;
using Ninject;

namespace ExpressionDemo.ConsoleApp
{
    public class Program
    {
        static void Main()
        {
            var kernel = new StandardKernel(new CountingApplicationModule());
            //var kernel = new StandardKernel(new AssemblyGeneratorModule());
            //var kernel = new StandardKernel(new CountingWithGeneratedAssembly());
            //var kernel = new StandardKernel(new FilterWithGeneratedAssemblyModule());
            //var kernel = new StandardKernel(new FilterWithCompiledFilterModule());
            //var kernel = new StandardKernel(new RpnCalculatorModule());
            
            var application = kernel.Get<IApplication>();
            application.Run();
        }
    }
}
