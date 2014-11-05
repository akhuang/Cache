using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Autofac;
using Zing.Core;

namespace UnitTest
{
    [TestFixture]
    public class UnitTest1
    {
        IContainer Container;

        [SetUp]
        public void Setup()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DefaultCacheManager>().As<ICacheManager>();
            Container = builder.Build();
        }

        [Test]
        public void TestMethod1()
        {
            ICacheManager cacheManager = Container.Resolve<ICacheManager>();

            cacheManager.Get<RegionEntity>(() =>
            {
                return new RegionEntity();
            });
        }
    }
}

