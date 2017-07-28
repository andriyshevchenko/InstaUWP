using GalaSoft.MvvmLight.Extensions.Xaml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;

namespace GalaSoft.MvvmLight.Extensions.Test.Xaml
{
    [TestClass]
    public class ViewMapTest
    {
        [UITestMethod]
        public void should_have_view()
        {
            var map =
                new ViewMap(
                    new PairList(
                        new Pair(FakeView.Type, new GalaSoft.MvvmLight.Extensions.TypeOf<object>().Value())
                    )
                );
            Assert.IsTrue(map.HasView(new object()));
        }

        [UITestMethod]
        public void should_get_view()
        {
            var map =
                new ViewMap(
                    new PairList(
                        new Pair(FakeView.Type, new GalaSoft.MvvmLight.Extensions.TypeOf<object>().Value())
                    )
                );
            Assert.IsTrue(map.GetViewFor(new object()).GetType() == FakeView.Type);
        }
    }
}
