using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static GalaSoft.MvvmLight.Extensions.HostViewModel;
using static System.Collections.Generic.Create;
using static System.Functional.Func;

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
            Assert.IsTrue(new Item(list(new object())).OnTop);
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

        private static Item Item()
        {
            var item = new Item(list(new object()));
            item.NewViewModel(new object());
            item.NewViewModel(new object());
            return item;
        }
    }

    public class FakeHost : HostViewModel
    {
        public FakeHost(string childName, object viewModel, int makeSteps = 2)
        {
            for (int i = 0; i < makeSteps; i++)
            {
                NavigateInternal("main", new object(), Direction.Forward);
            }
        }
    }

    [TestClass]
    public class HostViewModelTest
    {
        [TestMethod]
        public void should_trigger_property_changed()
        {
            var vm = new HostViewModel();
            vm.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != nameof(vm.Children))
                {
                    Assert.Fail("Property changed should be Children");
                }
            };
            vm.NavigateInternal("main", new object(), Direction.Forward);
        }

        [TestMethod]
        public void should_navigate_back_two_steps()
        {
            Assert.AreEqual(monad(new FakeHost("main", new object(), 4),
                            host => host.GoBack("main", 2))
                            .Items["main"]
                            .Position, 1);
        }

        [TestMethod]
        public void should_navigate_null_view_model()
        {
            var vm = monad(new FakeHost("main", new object(), 2), 
                           (host) => host.NavigateInternal("main", 4, Direction.Back));
            Assert.AreEqual(0, vm.Items["main"].Position);
        }

        [TestMethod]
        public void should_throw_exception_navigate_forward()
        {
            var vm = new HostViewModel();
            vm.NavigateInternal("main", new object(), Direction.Forward);
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => vm.NavigateInternal("main", new object(), Direction.Forward, 2));
        }

        [TestMethod]
        public void should_throw_exception_navigate_back()
        {
            var vm = new HostViewModel();
            vm.NavigateInternal("main", new object(), Direction.Forward);
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => vm.NavigateInternal("main", new object(), Direction.Back, 2));
        }

        [TestMethod]
        public void should_have_same_position_in_navigate_once()
        {
            Assert.AreEqual(0, ViewModel(1).Items["main"].Position);
        }


        [TestMethod]
        public void should_have_same_position_in_navigate_twice()
        {
            Assert.AreEqual(1, ViewModel(2).Items["main"].Position);
        }

        [TestMethod]
        public void should_have_one_view_model_in_navigate()
        {
            Assert.AreEqual(1, ViewModel(1).Items["main"].ViewModel.Count);
        }

        [TestMethod]
        public void should_have_two_view_model_in_navigate()
        {
            Assert.AreEqual(2, ViewModel(2).Items["main"].ViewModel.Count);
        }

        [TestMethod]
        public void should_have_three_view_models_in_navigate()
        {
            Assert.AreEqual(3, ViewModel(3).Items["main"].ViewModel.Count);
        }

        [TestMethod]
        public void should_have_four_view_models_in_navigate()
        {
            Assert.AreEqual(4, ViewModel(4).Items["main"].ViewModel.Count);
        }

        private static HostViewModel ViewModel(int makeSteps)
        {
            return new FakeHost("main", new object(), makeSteps);
        }
    }
}
