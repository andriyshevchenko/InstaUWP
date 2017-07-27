using Cactoos.Scalar;
using GalaSoft.MvvmLight.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Tests.Internal
{
    [TestClass]
    public class MergedAssemblyTypeCacheTest
    {
        [TestMethod]
        public void should_merge()
        {
            var mvvmLightExtensions =
                new CachedScalar<IReadOnlyDictionary<string, Type>>(
                    new AssemblyTypeCache(
                        new AssemblyOfType(typeof(HostViewModel))
                    )
                );

            var tests = 
                new CachedScalar<IReadOnlyDictionary<string, Type>>(
                    new AssemblyTypeCache(
                        new AssemblyOfType(typeof(MergedAssemblyTypeCacheTest))
                    )
                );


            Assert.AreEqual(
                new CachedScalar<IReadOnlyDictionary<string, Type>>(
                      new MergedTypeCache(
                          mvvmLightExtensions,
                          tests
                      )
                ).Value().Count,
                tests.Value().Count + mvvmLightExtensions.Value().Count
            );
        }
    }
}
