using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;

namespace GalaSoft.MvvmLight.Extensions.Test.Xaml
{
    [TestClass]
    public class ValueConverterGroupTest
    {
        [UITestMethod]
        public void should_pipeline_converters()
        {
            var converter = 
                new ValueConverterGroup(
                    new FakeStringToBoolConverter(), 
                    new FakeBoolToIntConverter()
                );
            Assert.AreEqual(1, converter.Convert("True", null, null, null));
        }
    }
}
