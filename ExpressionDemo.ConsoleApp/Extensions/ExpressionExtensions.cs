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
            Expression expr = null;

            foreach (Expression expression in expressions) {
                if (expr == null) {
                    expr = expression;
                    continue;
                }

                expr = Expression.OrElse(expr, expression);
            }

            return expr ?? Expression.Constant(false, typeof (bool));
        }

        public static Expression<Func<T, bool>> OrElse<T>(this IEnumerable<Expression<Func<T, bool>>> expressions)
        {
            Expression<Func<T, bool>> expr = null;

            foreach (Expression<Func<T, bool>> expression in expressions)
            {
                if (expr == null)
                {
                    expr = expression;
                    continue;
                }

                expr = expr.Or(expression);
            }

            return expr ?? (f => false);
        }

        public static Expression AndAlso(this IEnumerable<Expression> expressions)
        {
            Expression expr = null;

            foreach (Expression expression in expressions) {
                if (expr == null) {
                    expr = expression;
                    continue;
                }

                expr = Expression.AndAlso(expr, expression);
            }

            return expr ?? Expression.Constant(true, typeof (bool));
        }

        public static Expression<Func<T, bool>> AndAlso<T>(this IEnumerable<Expression<Func<T, bool>>> expressions)
        {
            Expression<Func<T, bool>> expr = null;

            foreach (Expression<Func<T, bool>> expression in expressions)
            {
                if (expr == null)
                {
                    expr = expression;
                    continue;
                }

                expr = expr.And(expression);
            }

            return expr ?? (f => true);
        }

    }
}