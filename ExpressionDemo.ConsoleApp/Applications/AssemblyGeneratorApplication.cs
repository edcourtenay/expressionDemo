// using System;
// using System.Reflection;
// using System.Reflection.Emit;
// using ExpressionDemo.Common;
// using ExpressionDemo.ConsoleApp.Filters;
//
// namespace ExpressionDemo.ConsoleApp.Applications
// {
//     internal class AssemblyGeneratorApplication : IApplication
//     {
//         private const string AssemblyFilename = "TestAssembly.dll";
//         private readonly ExpressionFilter _filter;
//
//         public AssemblyGeneratorApplication(ExpressionFilter filter)
//         {
//             _filter = filter ?? throw new ArgumentNullException(nameof(filter));
//         }
//
//         public void Run()
//         {
//             var expr = _filter.GetFilterExpression();
//
//             var assembly =
//                 AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName("TestAssembly"),
//                     AssemblyBuilderAccess.RunAndSave);
//             var mod = assembly.DefineDynamicModule("TestModule", AssemblyFilename, true);
//             var type = mod.DefineType("TestType", TypeAttributes.Public, typeof(object), new []{typeof(IFilterImplementation)});
//             
//             var staticMethod = type.DefineMethod("FilterStatic", MethodAttributes.Public | MethodAttributes.Static,
//                 typeof(bool), new[] { typeof(IGeoDataLocation) });
//
//             expr.CompileToMethod(staticMethod);
//
//             GenerateInterfaceWrapper(type, staticMethod);
//             SaveAssembly(type, assembly);
//         }
//
//         private static void SaveAssembly(TypeBuilder type, AssemblyBuilder assembly)
//         {
//             var dt = type.CreateType();
//             assembly.Save(AssemblyFilename);
//         }
//
//         private static void GenerateInterfaceWrapper(TypeBuilder type, MethodBuilder staticMethod)
//         {
//             var method = type.DefineMethod("Filter", MethodAttributes.Public | MethodAttributes.Virtual,
//                 typeof (bool), new[] {typeof (IGeoDataLocation)});
//
//             var generator = method.GetILGenerator();
//
//             generator.Emit(OpCodes.Nop);
//             generator.Emit(OpCodes.Ldarg_1);
//             generator.EmitCall(OpCodes.Call, staticMethod, new[] {typeof (IGeoDataLocation)});
//             generator.Emit(OpCodes.Ret);
//         }
//     }
// }