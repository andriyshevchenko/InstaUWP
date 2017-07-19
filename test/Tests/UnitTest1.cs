using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GalaSoft.MvvmLight.Extensions;

using static System.Collections.Generic.Create;
using InputValidation;

namespace Tests
{
    [TestClass]
    public class LinqExpressionCtorTest
    {
        [TestMethod]
        public void should_create_from_type_and_args()
        {
            Assert.IsTrue(new LinqExpressionCtor(typeof(string), array<object>(array('1', '2'))).Value().As<string>() == "12";
        }
    }
}
