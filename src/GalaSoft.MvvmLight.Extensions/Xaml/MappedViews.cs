using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using InputValidation;
using static System.Collections.Generic.Create;
using static System.Functional.Func;
using System.Linq;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    public class MappedViews
    {
        ViewMapItemCollection _items;
        public Dictionary<Type, Func<Page>> ToDictionary()
        {
            return dictionary(_items.Select(item => (item.ViewModel, fun(() => Activator.CreateInstance(item.View).As<Page>()))));
        }
        public MappedViews(ViewMapItemCollection items)
        {
            _items = items;
        }
    }
}
