using System;
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
            var item = new Item(list(new object()));
            item.NewViewModel(new object());
            Assert.IsTrue(item.OnTop);
        }

        [TestMethod]
        public void should_determine_is_on_top()
        {
            var item = new Item(list(new object()));
            Assert.IsTrue(item.OnTop);
        }

        [TestMethod]
        public void should_have_same_number_of_view_models_in_item()
        {
            Item item = Item();
            Assert.AreEqual(item.ViewModel.Count, 3);
        }

        [TestMethod]
        public void should_have_same_position_in_item()
        {
            Item item = Item();
            Assert.AreEqual(item.Position, 2);
        }

        private static Item Item()
        {
            var item = new Item(list(new object()));
            item.NewViewModel(new object());
            item.NewViewModel(new object());
            return item;
        }
    }

    [TestClass]
    public class HostViewModelTest
    {
        [TestMethod]
        public void should_throw_exception_navigate_forward()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => ViewModel().NavigateInternal("main", new object(), Direction.Forward, 2));
        }

        [TestMethod]
        public void should_throw_exception_navigate_back()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => new HostViewModel("main", new object()).NavigateInternal("main", new object(), Direction.Back, 2));
        }

        [TestMethod]
        public void should_have_same_position_in_navigate()
        {
            HostViewModel vm = v ViewModel();
            Assert.AreEqual(2, vm.Items["main"].Position);
        }

        [TestMethod]
        public void should_have_same_number_of_view_models_in_navigate()
        {
            HostViewModel vm = ViewModel();
            Assert.AreEqual(3, vm.Items["main"].ViewModel.Count);
        }

        private static HostViewModel ViewModel()
        {
            var vm = new HostViewModel("main", new object());
            vm.NavigateInternal("main", new object(), Direction.Forward);
            vm.NavigateInternal("main", new object(), Direction.Forward);
            return vm;
        }
    }
}
