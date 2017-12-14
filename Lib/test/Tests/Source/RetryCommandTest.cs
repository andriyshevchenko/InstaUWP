using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GalaSoft.MvvmLight.Extensions;

namespace Tests.Source
{
    [TestClass]
    public class RetryCommandTest
    {
        [TestMethod]
        public void should_retry_three_times()
        {
            int i = 0;
            var command =
                new RetryCommand(() =>
                {
                    if (i < 3)
                    {
                        i++;
                        throw new NotFiniteNumberException();
                    }
                },
                4);
            command.Execute(1);
            Assert.AreEqual(3, command.Errors().Length);
        }
    }
}
