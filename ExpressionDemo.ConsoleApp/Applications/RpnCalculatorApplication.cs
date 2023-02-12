using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ExpressionDemo.Common;

namespace ExpressionDemo.ConsoleApp.Applications
{
    internal class RpnCalculatorApplication : IApplication
    {
        public void Run()
        {
            // const string formula = "2 2 +";
            // const string formula = "512 12 4 / root 4 - 2 ^";
            // const string formula = "512 1 12 4 / / ^ 4 - 2 ^"; 
            const string formula = "2 3 + 4 * 5 +";
            
            var tokens = formula.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            Expression UnaryMath(string s, Expression x) =>
                Expression.Call(typeof(Math).GetMethod(s, new[] {typeof(double)}), x);

            Expression BinaryMath(string s, Expression x, Expression y) =>
                Expression.Call(typeof(Math).GetMethod(s, new[] {typeof(double), typeof(double)}), x, y);

            var constantMap = new Dictionary<string, double>
            {
                {"pi", Math.PI}
            };

            var binaryMap = new Dictionary<string, Func<Expression, Expression, Expression>>
            {
                {"+", Expression.Add},
                {"-", Expression.Subtract},
                {"*", Expression.Multiply},
                {"/", Expression.Divide},
                {"^", Expression.Power},
                {"mod", Expression.Modulo},
                {"log", (x, y) => BinaryMath("Log", x, y)},
                {"min", (x, y) => BinaryMath("Min", x, y)},
                {"max", (x, y) => BinaryMath("Max", x, y)},
                {"root", (x, y) => Expression.Power(x, Expression.Divide(Expression.Constant(1d), y))}
            };

            var unaryMap = new Dictionary<string, Func<Expression, Expression>>
            {
                {"abs", x => UnaryMath("Abs", x)},
                {"exp", x => UnaryMath("Exp", x)},
                {"sin", x => UnaryMath("Sin", x)},
                {"cos", x => UnaryMath("Cos", x)},
                {"tan", x => UnaryMath("Tan", x)},
                {"sqrt", x => UnaryMath("Sqrt", x)}
            };

            var stack = new Stack<Expression>();

            foreach (var token in tokens)
            {
                if (double.TryParse(token, out var d))
                {
                    stack.Push(Expression.Constant(d));
                    continue;
                }
                if (constantMap.ContainsKey(token))
                {
                    stack.Push(Expression.Constant(constantMap[token]));
                    continue;
                }
                if (binaryMap.ContainsKey(token))
                {
                    var func = binaryMap[token];
                    var y = stack.Pop();
                    var x = stack.Pop();
                    stack.Push(func(x, y));
                    continue;
                }
                if (unaryMap.ContainsKey(token))
                {
                    var func = unaryMap[token];
                    stack.Push(func(stack.Pop()));
                }
            }

            var expression = stack.Pop();

            Console.WriteLine(expression.ToString());

            Dump(expression);
            
            var lambda = Expression.Lambda<Func<double>>(expression);
            Console.WriteLine("\n= {0}", lambda.Compile().Invoke());
        }

        public void Dump(Expression expression, int depth = 0)
        {
            Console.WriteLine("{0}{1}", new string(' ', depth * 2),
                ExpressionLabel(expression));

            switch (expression)
            {
                case BinaryExpression binaryExpression:
                    Dump(binaryExpression.Left, depth + 1);
                    Dump(binaryExpression.Right, depth + 1);
                    break;
                case MethodCallExpression methodCallExpression:
                {
                    foreach (var argument in methodCallExpression.Arguments)
                    {
                        Dump(argument, depth + 1);
                    }

                    break;
                }
            }
        }

        private static string ExpressionLabel(Expression expression)
        {
            switch (expression)
            {
                case ConstantExpression expr:
                    return expr.Value.ToString();

                case MethodCallExpression expr:
                    return expr.Method.Name;

                default:
                    return expression.NodeType.ToString();
            }
        }
    }
}