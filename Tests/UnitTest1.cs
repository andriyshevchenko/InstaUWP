using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace GalaSoft.MvvmLight.Extensions.Test
{
    [TestClass]
    public class HostViewModelTest
    {
        [TestMethod]
        public void should_throw_exception_navigate_internal()
        {
            var viewModel = new HostViewModel();
            viewModel.NavigateInternal("main", new object(), HostViewModel.Direction.Forward);

            ThrowsException<ArgumentOutOfRangeException>(
                () => viewModel.NavigateInternal("main", new object(), HostViewModel.Direction.Back, 2));
        }
    }
}
