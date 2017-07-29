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
                   new TypeCacheWithoutNamespace(
                       new MergedTypeCache(
                           assemblyOfType,
                           assemblyOfType2
                       )
                   ),
                   nameof(InferredNameTest)
                );
            Assert.AreEqual("Tests.Source.InferredNameTest", inferredName.Value());
        }
    }
}
