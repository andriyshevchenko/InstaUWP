using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GalaSoft.MvvmLight.Extensions;

using static System.Functional.Func;

namespace Tests.Source
{
    [TestClass]
    public class ErrorSafeCommandTest
    {
        [TestMethod]
        public void should_be_error_safe()
        {
            var command = new ErrorSafeCommand(() => throw new NotFiniteNumberException());
            Assert.IsTrue(
                monad(
                    command,
                    cmd => cmd.Execute(null)
                )
        }
    }
}
