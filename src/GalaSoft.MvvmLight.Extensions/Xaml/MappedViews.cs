using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using InputValidation;

using static System.Collections.Generic.Create;
using static System.Functional.Func;
using static System.Functional.FlowControl;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    public class MappedViews
    {
        private readonly PairCollection _items;

        public Dictionary<Type, Func<UserControl>> ToDictionary()
        {
            return dictionary(
                       map(
                           _items,
                           item => (
                               item.ViewModel,
                               fun(() => new LinqExpressionCtor(item.View).Value().As<UserControl>())
                           )
                        )
                   );
        }

        public MappedViews(PairCollection items)
        {
            each(items.Views, item => item.CheckNotNull("Null View type occured, please correct your Xaml"));
            each(items.ViewModels, item => item.CheckNotNull("Null ViewModel type occured, please correct your Xaml"));
            _items = items;
        }
    }
}
