using InputValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Windows.UI.Xaml.Controls;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    /// <summary>
    /// A <see cref="IViewMap"/> which is created from <see cref="IPair"/>'s.
    /// </summary>
    public class ViewMap : IViewMap
    {
        private Dictionary<Type, Func<UserControl>> Views => _lazy.Value;
        private Lazy<Dictionary<Type, Func<UserControl>>> _lazy;

        /// <summary>
        /// Initializes a new instance of <see cref="ViewMap"/>.
        /// </summary>
        /// <param name="map">The pairs.</param>
        public ViewMap(IList<object> map)
            : this(map.Cast<IPair>()
                      .ToList()
                      .As<IList<IPair>>()
              )
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="ViewMap"/>.
        /// </summary>
        /// <param name="map">The pairs.</param>
        public ViewMap(IList<Pair> map) : this(map.Cast<IPair>().ToList())
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="ViewMap"/>.
        /// </summary>
        /// <param name="map">The pairs.</param>
        public ViewMap(IList<IPair> map)
        {
            _lazy = new Lazy<Dictionary<Type, Func<UserControl>>>(
                () => new PairsAsDictionary(map).Value()
            );
        }

        /// <summary>
        /// Gets a new view instance for specific view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>New view instance.</returns>
        public object GetViewFor(object viewModel)
        {
            Type type = viewModel.GetType();

            if (Views.ContainsKey(type))
            {
                return new LazyUserControl(Views[type], viewModel).Value();
            }
            else
            {
                //check if base type is present in dictionary
                TypeInfo typeInfo = type.GetTypeInfo();
                Type baseType = typeInfo.BaseType;
                if (Views.ContainsKey(baseType))
                {
                    //redirect to base type view 
                    Views[type] = Views[baseType];
                    return new LazyUserControl(Views[baseType], viewModel).Value();
                }

                //check if dictionary contains any of implemented
                var interfaces = type.GetInterfaces();
                if (interfaces.Any())
                {
                    foreach (var iface in interfaces)
                    {
                        if (Views.ContainsKey(iface))
                        {
                            //redirect to implemented interface view
                            Views[type] = Views[iface];
                            return new LazyUserControl(Views[iface], viewModel).Value();
                        }
                    }
                }
            }
            throw new InvalidOperationException($"View for requested view model {type} not added yet");
        }

        /// <summary>
        /// Determines if type of view model has a corresponding view.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>True if specific type of view model has view defined.</returns>
        public bool HasView(object viewModel)
        {
            return Views.ContainsKey(viewModel.GetType());
        }
    }
}
