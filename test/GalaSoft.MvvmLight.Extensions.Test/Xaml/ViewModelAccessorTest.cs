using GalaSoft.MvvmLight.Extensions.Xaml;
using InputValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static System.Collections.Generic.Create;

namespace GalaSoft.MvvmLight.Extensions.Xaml.Tests
{
    [TestClass]
    public class ViewModelAccessorTest
    {
        [TestMethod]
        public void should_get_view_model_for_required_child_name()
        {
            object test = new object();
            Assert.IsTrue(
                new ViewModelAccessor()
                .Convert(dictionary(("main", test)), null, "main", null) == test);
        }
    }
}
