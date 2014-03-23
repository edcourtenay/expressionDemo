using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LinqKit;

namespace ExpressionDemo.ConsoleApp.Extensions
{
    public static class ExpressionExtensions
    { 
        public static Expression OrElse(this IEnumerable<Expression> expressions)
        {
            return Combine(expressions, Expression.OrElse, false);
        }

        public static Expression AndAlso(this IEnumerable<Expression> expressions)
        {
            return Combine(expressions, Expression.AndAlso, true);
        }

        public static Expression<Func<T, bool>> OrElse<T>(this IEnumerable<Expression<Func<T, bool>>> expressions)
        {
            return Combine(expressions, (left, right) => left.Or(right), false);
        }

        public static Expression<Func<T, bool>> AndAlso<T>(this IEnumerable<Expression<Func<T, bool>>> expressions)
        {
            return Combine(expressions, (left, right) => left.And(right), false);
        }

        private static Expression Combine(IEnumerable<Expression> expressions, Func<Expression, Expression, Expression> combiner, bool initialValue)
        {
            var stack = new Stack<Expression>(expressions);

            if (stack.Count == 0)
                stack.Push(Expression.Constant(initialValue, typeof (bool)));

            while (stack.Count > 1) {
                var right = stack.Pop();
                var left = stack.Pop();
                stack.Push(combiner(left, right));
            }

            return stack.Pop();
        }

        private static Expression<Func<T, bool>> Combine<T>(IEnumerable<Expression<Func<T, bool>>> expressions,
            Func<Expression<Func<T, bool>>, Expression<Func<T, bool>>, Expression<Func<T, bool>>> combiner, bool initialValue)
        {
            var stack = new Stack<Expression<Func<T, bool>>>(expressions);

            if (stack.Count == 0)
                stack.Push(f => initialValue);

            while (stack.Count > 1) {
                var right = stack.Pop();
                var left = stack.Pop();
                stack.Push(combiner(left, right));
            }

            return stack.Pop();
        }
    }
}