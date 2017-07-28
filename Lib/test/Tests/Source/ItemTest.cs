using Microsoft.VisualStudio.TestTools.UnitTesting;
using static GalaSoft.MvvmLight.Extensions.HostViewModel;
using static System.Collections.Generic.Create;

namespace GalaSoft.MvvmLight.Extensions.Test
{
    [TestClass]
    public class ItemTest
    {
        [TestMethod]
        public void should_determine_is_on_top_after_add()
        {
            var item = new Entry(list(new object()));
            item.NewViewModel(new object());
            Assert.IsTrue(item.OnTop);
        }

        [TestMethod]
        public void should_determine_is_on_top()
        {
            Assert.IsTrue(new Entry(list(new object())).OnTop);
        }

        [TestMethod]
        public void should_have_same_number_of_view_models_in_item()
        {
            Assert.AreEqual(Item().ViewModel.Count, 3);
        }

        [TestMethod]
        public void should_have_same_position_in_item()
        {
            Assert.AreEqual(Item().Position, 2);
        }

        private static Entry Item()
        {
            var item = new Entry(list(new object()));
            item.NewViewModel(new object());
            item.NewViewModel(new object());
            return item;
        }
    }
}
