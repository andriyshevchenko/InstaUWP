using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using InputValidation;
using System.Linq;

using static System.Collections.Generic.Create;
using static System.Functional.Func;
using static System.Linq.Enumerable;
using static System.Functional.FlowControl;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    public class MappedViews
    {
        private readonly ViewMapItemCollection _items;

        public Dictionary<Type, Func<UserControl>> ToDictionary()
        {
            return dictionary(
                       map(
                           _items,
                           item => (
                               item.ViewModel, 
                               fun(() => Activator.CreateInstance(item.View).As<UserControl>())
                           )
                        )
                   );
        }

        public MappedViews(ViewMapItemCollection items)
        {
            each(items.Views, item => item.CheckNotNull("Null View type occured, please correct your Xaml"));
            each(items.ViewModels, item => item.CheckNotNull("Null ViewModel type occured, please correct your Xaml"));
            _items = items;
        }
    }
}
