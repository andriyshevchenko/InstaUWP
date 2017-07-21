using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GalaSoft.MvvmLight.Extensions.Xaml;

using static System.Collections.Generic.Create;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;

namespace GalaSoft.MvvmLight.Extensions.Test.Xaml
{
    [TestClass]
    public class MappedViewsTest
    {
        [UITestMethod]
        public void should_convert_mapped_views_to_dictionary()
        {
            Type type = typeof(object);
            Assert.IsTrue(
                new MappedViews(
                    new PairCollection(
                        array(new Pair(FakeView.Type, type))
                    )
                ).ToDictionary()[type].Invoke().GetType() == FakeView.Type);
        }
    }
}
