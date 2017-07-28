using Microsoft.VisualStudio.TestTools.UnitTesting;
using GalaSoft.MvvmLight.Extensions;
using System.Linq;

namespace Tests.Source
{
    [TestClass]
    public class NamespacesOfAssemblyTest
    {
        [TestMethod]
        public void should_get_namespaces()
        {
            var namespaces =
                new NamespacesOfAssembly(
                    new AssemblyOfType<HostViewModel>()
                );
            Assert.AreEqual(4, namespaces.Value().Count());
        }
    }
}
