using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using ExpressionDemo.ConsoleApp.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace ExpressionDemo.ConsoleApp.Tests.Unit
{
    [TestFixture]
    class ExpressionExtensionsTest
    {
        [TestCase(true, 0)]
        [TestCase(false, 1)]
        public void OrElseShouldCreateTreeThatShortCircuitEvaluatesInTheCorrectOrder(bool firstReturn, int expectedInvocationCount)
        {
            var guid = Guid.NewGuid();
            var block = BlockExpression(true, guid);

            var expression = new Expression[] {
                Expression.Constant(firstReturn, typeof(bool)),
                block
            }.OrElse();

            var action = Expression.Lambda<Func<bool>>(expression);

            InvocationCounter.Reset(guid);
            action.Compile().Invoke().Should().BeTrue();
            InvocationCounter.Count(guid).Should().Be(expectedInvocationCount);
        }

        private BlockExpression BlockExpression(bool value, Guid guid)
        {
            var returnTarget = Expression.Label(typeof (bool));

            return
                Expression.Block(
                    Expression.Call(typeof (InvocationCounter).GetMethod("Increment", new[] { typeof (Guid) }),
                        Expression.Constant(guid)),
                    Expression.Label(returnTarget, Expression.Constant(value, typeof (bool))));
        }

        public static class InvocationCounter
        {
            private static readonly ConcurrentDictionary<Guid, InnerCounter> Dict =
                new ConcurrentDictionary<Guid, InnerCounter>();

            public static int Count(Guid guid)
            {
                return GetInvocationCounter(guid).Count;
            }

            public static void Increment(Guid guid)
            {
                GetInvocationCounter(guid).Increment();
            }

            public static void Reset(Guid guid)
            {
                GetInvocationCounter(guid).Reset();
            }

            private static InnerCounter GetInvocationCounter(Guid guid)
            {
                return Dict.GetOrAdd(guid, _ => new InnerCounter());
            }

            public class InnerCounter
            {
                private int _count;

                public int Count { get { return _count; } }

                public void Reset()
                {
                    _count = 0;
                }

                public void Increment()
                {
                    _count++;
                }
            }
        }
    }
}
