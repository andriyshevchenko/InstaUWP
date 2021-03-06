namespace GalaSoft.MvvmLight.Extensions.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using static GalaSoft.MvvmLight.Extensions.HostViewModel;
    using static System.Functional.Func;

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
            Assert.AreEqual(
                monad(
                    new FakeHost("main", new object(), 4),
                    host => host.GoBack("main", 2)
                ).Items["main"].Position, 
                1
            );
        }

        [TestMethod]
        public void should_navigate_null_view_model()
        {
            Assert.AreEqual(
                0,
                monad(
                    new FakeHost("main", new object(), 2),
                    host => host.NavigateInternal("main", 4, Direction.Back)
                ).Items["main"].Position
            );
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

        [TestMethod]
        public void should_go_back()
        {
            var viewModel = new HostViewModel();
            object vm = new object();
            viewModel.NavigateTo("main", vm);
            viewModel.NavigateTo("main", new object());
            viewModel.GoBack("main");
        }

        private static HostViewModel ViewModel(int makeSteps)
        {
            return new FakeHost("main", new object(), makeSteps);
        }
    }
}
