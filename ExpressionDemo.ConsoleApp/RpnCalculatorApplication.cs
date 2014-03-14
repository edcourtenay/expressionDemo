using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ExpressionDemo.Common;

namespace ExpressionDemo.ConsoleApp
{
    internal class RpnCalculatorApplication : IApplication
    {
        public void Run()
        {
            string formula = "512 12 4 / root 4 - 2 ^";
            //string formula = "512 1 12 4 / / ^ 4 - 2 ^"; 
            //string formula = "3 4 5 6 - + *";
            
            string[] tokens = formula.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            Func<string, Expression, Expression> unaryMath = (s, x) =>
                Expression.Call(typeof (Math).GetMethod(s, new[] {typeof (double)}), x);

            Func<string, Expression, Expression, Expression> binaryMath = (s, x, y) =>
                Expression.Call(typeof (Math).GetMethod(s, new[] {typeof (double), typeof (double)}), x, y);

            var binaryMap = new Dictionary<string, Func<Expression, Expression, Expression>>
            {
                {"+", Expression.Add},
                {"-", Expression.Subtract},
                {"*", Expression.Multiply},
                {"/", Expression.Divide},
                {"^", Expression.Power},
                {"mod", Expression.Modulo},
                {"log", (x, y) => binaryMath("Log", x, y)},
                {"min", (x, y) => binaryMath("Min", x, y)},
                {"max", (x, y) => binaryMath("Max", x, y)},
                {"root", (x, y) => Expression.Power(x, Expression.Divide(Expression.Constant(1d), y))}
            };

            var unaryMap = new Dictionary<string, Func<Expression, Expression>>
            {
                {"abs", x => unaryMath("Abs", x)},
                {"exp", x => unaryMath("Exp", x)},
                {"sin", x => unaryMath("Sin", x)},
                {"cos", x => unaryMath("Cos", x)},
                {"tan", x => unaryMath("Tan", x)},
                {"sqrt", x => unaryMath("Sqrt", x)}
            };

            var stack = new Stack<Expression>();

            foreach (string token in tokens)
            {
                double d;
                if (double.TryParse(token, out d))
                {
                    stack.Push(Expression.Constant(d));
                    continue;
                }
                if (binaryMap.ContainsKey(token))
                {
                    Func<Expression, Expression, Expression> func = binaryMap[token];
                    Expression y = stack.Pop();
                    Expression x = stack.Pop();
                    stack.Push(func(x, y));
                    continue;
                }
                if (unaryMap.ContainsKey(token))
                {
                    Func<Expression, Expression> func = unaryMap[token];
                    stack.Push(func(stack.Pop()));
                }
            }

            Expression expression = stack.Pop();

            Console.WriteLine(expression.ToString());
            
            Dump(expression);
            
            Expression<Func<double>> lambda = Expression.Lambda<Func<double>>(expression);
            Console.WriteLine("\n= {0}", lambda.Compile().Invoke());
        }

        public void Dump(Expression expression, int depth = 0)
        {
            Console.WriteLine("{0}{1}", new String(' ', depth * 2), expression.NodeType != ExpressionType.Constant ? expression.NodeType.ToString() : ((ConstantExpression) expression).Value.ToString());

            var binaryExpression = expression as BinaryExpression;
            if (binaryExpression != null)
            {
                Dump(binaryExpression.Left, depth + 1);
                Dump(binaryExpression.Right, depth + 1);
            }
        }
    }
}