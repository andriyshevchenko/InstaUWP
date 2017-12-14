using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using InputValidation;
using Cactoos;

using static System.Collections.Generic.Create;
using static System.Functional.Func;
using Cactoos.Reflection;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    /// <summary>
    /// Convert pairs to <see cref="Dictionary{TKey, TValue}"/>.
    /// Key: view model type, value: function, which return a new <see cref="UserControl"/> instance.
    /// </summary>
    public class PairsAsDictionary : IScalar<Dictionary<Type, Func<UserControl>>>
    {
        private readonly IList<IPair> items;

        /// <summary>
        /// Gets the dictionary.
        /// </summary>
        /// <returns>New dictionary instance.</returns>
        public Dictionary<Type, Func<UserControl>> Value()
        {
            var dictionary = new Dictionary<Type, Func<UserControl>>(items.Count);
            for (int i = 0; i < items.Count; i++)
            {
                var pair = items[i];
                dictionary.Add(
                    pair.ViewModelType, 
                    () => (UserControl)new FastObject(pair.ViewType).Value()
                );
            }
            return dictionary;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="PairsAsDictionary"/>.
        /// </summary>
        /// <param name="items">The pairs.</param>
        public PairsAsDictionary(IList<IPair> items)
        {
            this.items = items;
        }
    }
}
