using GalaSoft.MvvmLight.Extensions.Xaml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;

using static System.Collections.Generic.Create;

namespace GalaSoft.MvvmLight.Extensions.Test.Xaml
{
    [TestClass]
    public class ViewModelConverterPipeTest
    {
        [UITestMethod]
        public void should_convert_children_dictionary_to_view()
        {
            var converter =
                new ViewModelConverterPipe(
                    new PairList(
                        new Pair(FakeView.Type, typeof(object))
                    )
                );
            var parameter = "main";
            var children = dictionary((parameter, new object()));

            var result = converter.Convert(children, null, parameter, null);
            Assert.IsTrue(result.GetType() == FakeView.Type);
        }
    }
}
