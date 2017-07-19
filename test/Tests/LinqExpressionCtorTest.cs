using Microsoft.VisualStudio.TestTools.UnitTesting;
using GalaSoft.MvvmLight.Extensions;
using InputValidation;

using static System.Collections.Generic.Create;

namespace Tests
{
    [TestClass]
    public class LinqExpressionCtorTest
    {
        [TestMethod]
        public void should_create_from_type_and_args()
        {
            Assert.IsTrue(new LinqExpressionCtor(typeof(string), 6, array('1', '2')).Value().As<string>() == "12");
        }
    }
}
