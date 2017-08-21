using Microsoft.VisualStudio.TestTools.UnitTesting;
using GalaSoft.MvvmLight.Extensions;
using Cactoos.Reflection;
using Cactoos.Text;

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
                   new SimpleNameTypeCache(
                       new MergedTypeCache(
                           assemblyOfType,
                           assemblyOfType2
                       )
                   ),
                   nameof(InferredNameTest)
                );
            Assert.AreEqual("Tests.Source.InferredNameTest", inferredName.String());
        }
    }
}
