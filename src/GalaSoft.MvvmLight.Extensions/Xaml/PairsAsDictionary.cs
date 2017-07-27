using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using InputValidation;

using static System.Collections.Generic.Create;
using static System.Functional.Func;
using Cactoos;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    public class PairsAsDictionary : IScalar<Dictionary<Type, Func<UserControl>>>
    {
        private readonly IList<IPair> items;

        public Dictionary<Type, Func<UserControl>> Value()
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

        public PairsAsDictionary(IList<IPair> items)
        {
            this.items = items;
        }
    }
}
