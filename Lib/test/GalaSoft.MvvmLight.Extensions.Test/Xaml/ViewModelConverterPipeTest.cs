using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;

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
                        new Pair(FakeView.Type, new GalaSoft.MvvmLight.Extensions.TypeOf<object>().Value())
                    )
                );
            var parameter = "main";
            var children = dictionary((parameter, new object()));

            var result = converter.Convert(children, null, parameter, null);
            Assert.IsTrue(result.GetType() == FakeView.Type);
        }
    }
}
