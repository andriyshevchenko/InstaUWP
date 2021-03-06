using Cactoos.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Source
{
    [TestClass]
    public class NamespacedNameTest
    {
        [TestMethod]
        public void should_add_namespace_if_no_present()
        {
            var name = "Pair";
            var @namespace = "GalaSoft.MvvmLight.Xaml";
            Assert.AreEqual($"{@namespace}.{name}", new NamespacedName(name, @namespace).String());
        }

        [TestMethod]
        public void should_not_add_namespace()
        {
            var name = "GalaSoft.MvvmLight.Xaml.Pair";
            var @namespace = "GalaSoft.MvvmLight.Xaml";
            Assert.AreEqual(name, new NamespacedName(name, @namespace).String());
        }
    }
}
