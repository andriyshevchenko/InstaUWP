using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using InputValidation;

using static System.Collections.Generic.Create;
using static System.Functional.Func;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    public class MappedViews
    {
        private readonly IList<IPair> items;

        public Dictionary<Type, Func<UserControl>> ToDictionary()
        {
            return dictionary(
                       map(
                           items,
                           item => (
                               item.ViewModel,
                               fun(() => new FastObjectCreation(item.View).Value().As<UserControl>())
                           )
                        )
                   );
        }

        public MappedViews(IList<IPair> items)
        {
            this.items = items;
        }
    }
}
