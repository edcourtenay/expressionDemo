using System;
using ExpressionDemo.Common;
using ExpressionDemo.ConsoleApp.Ninject;
using Ninject;

namespace ExpressionDemo.ConsoleApp
{
    public class Program
    {
        static void Main()
        {
            var kernel = new StandardKernel(
                new DataSourceModule(),
                new ConfigurationModule(),
                //new AssemblyGeneratorModule()
                new CountingApplicationModule()
            );
            // var kernel = new StandardKernel(new CountingWithGeneratedAssembly());
            // var kernel = new StandardKernel(new FilterWithGeneratedAssemblyModule());
            // var kernel = new StandardKernel(new FilterWithCompiledFilterModule());
            // var kernel = new StandardKernel(new RpnCalculatorModule());

            var application = kernel.GetAll<IApplication>();

            foreach (var a in application)
            {
                var name = a.GetType().Name;
                Console.WriteLine(name);
                Console.WriteLine(new string('=', name.Length));
                Console.WriteLine();

                a.Run();
            }
        }
    }
}
