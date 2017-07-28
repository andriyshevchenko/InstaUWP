using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using InputValidation;
using Cactoos;

using static System.Collections.Generic.Create;
using static System.Functional.Func;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    /// <summary>
    /// Convert pairs to <see cref="Dictionary{TKey, TValue}"/>.
    /// Key: view model type, value: function, which return a new <see cref="UserControl"/> instance.
    /// </summary>
    public class PairsAsDictionary : IScalar<Dictionary<Type, Func<UserControl>>>
    {
        /// <summary>
        /// Reuse the instances.
        /// </summary>
        private static Dictionary<Type, UserControl> _instances 
            = new Dictionary<Type, UserControl>();

        private readonly IList<IPair> items;

        /// <summary>
        /// Gets the dictionary.
        /// </summary>
        /// <returns>New dictionary instance.</returns>
        public Dictionary<Type, Func<UserControl>> Value()
        {
            return dictionary(
                       map(
                           items,
                           item => (
                               item.ViewModel,
                               fun(() => 
                               {
                                   Type type = item.View;

                                   if (_instances.ContainsKey(type))
                                   {
                                       return _instances[type];
                                   }

                                   _instances[type] = new FastObject(type)
                                       .Value()
                                       .As<UserControl>();

                                   return _instances[type];
                               })
                           )
                        )
                   );
        }

        /// <summary>
        /// Initializes a new instance of <see cref="PairsAsDictionary"/>
        /// </summary>
        /// <param name="items">The pairs.</param>
        public PairsAsDictionary(IList<IPair> items)
        {
            this.items = items;
        }
    }
}
