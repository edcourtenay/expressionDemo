using System;
using System.Collections.Generic;
using System.Linq;
using ExpressionDemo.Common;
using FluentAssertions;
using Moq;
using Ninject;
using Ninject.MockingKernel.Moq;
using NUnit.Framework;

namespace ExpressionDemo.ConsoleApp.Tests.Unit
{
    [TestFixture]
    public class FilterBehaviourTests
    {
        public static IEnumerable<Type> Filters()
        {
            return typeof(Program).Assembly.DefinedTypes
                .Where(type => type.Name.EndsWith("Filter"));
        }

        public static IEnumerable<Type> Configurations()
        {
            return typeof(Program).Assembly.DefinedTypes
                .Where(type => type.Name.EndsWith("Configuration"))
                .Where(type => !type.IsAbstract);
        }

        public static IEnumerable<object[]> FiltersAndConfigurations()
        {
            return Filters()
                .SelectMany(filter => Configurations(),
                    (filter, configuration) => new object[] { filter, configuration });
        }

// ReSharper disable ClassNeverInstantiated.Local
        private class FakeConfiguration : IConfiguration
        {
            public IEnumerable<string> CountryCodes
            {
                get { return new[] {"gb"}; }
            }

            public IEnumerable<Tuple<string, string>> Features
            {
                get { return new[] {new Tuple<string, string>("p", "ppl")}; }
            }

            public long? MinimumPopulationSize
            {
                get { return 100000L; }
            }

            public DateTime? ModifiedAfter
            {
                get { return null; }
            }
        }
// ReSharper restore ClassNeverInstantiated.Local

        [Test]
        [TestCaseSource(nameof(FiltersAndConfigurations))]
        public void AllFiltersShouldExecuteWithAllConfigurations(Type filterType, Type configurationType)
        {
            var kernel = new MoqMockingKernel();
            kernel.Bind<IFilter>().To(filterType);
            kernel.Bind<IConfiguration>().To(configurationType);

            var filter = kernel.Get<IFilter>();

            var geoDataLocation = Mock.Of<IGeoDataLocation>(l => l.CountryCode == "gb" &&
                l.FeatureClass == "p" && l.FeatureCode == "ppl" &&
                l.Population == 200000L &&
                l.ModificationDate == new DateTime(2012, 1, 1));

            Action action = () => filter.GetFilterFunction().Invoke(geoDataLocation);

            action.Should().NotThrow();
        }
    }
}