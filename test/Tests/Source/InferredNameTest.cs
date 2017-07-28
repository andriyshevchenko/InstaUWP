using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GalaSoft.MvvmLight.Extensions;

namespace Tests.Source
{
    [TestClass]
    public class InferredNameTest
    {
        [TestMethod]
        public void should_infer()
        {
            var assemblyOfType = new AssemblyOfType<HostViewModel>();
            var assemblyOfType2 = new AssemblyOfType<InferredNameTest>();
            var inferredName =
                new InferredName(
                    nameof(InferredNameTest),
                    (
                        new AssemblyRootNamespace(assemblyOfType),
                        new AssemblyTypeCache(assemblyOfType)
                    ),
                    (
                        new AssemblyRootNamespace(assemblyOfType2),
                        new AssemblyTypeCache(assemblyOfType2)
                    )
                );
            Assert.AreEqual("Tests.Source.InferredNameTest", inferredName.Value());
        }
    }
}
