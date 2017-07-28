using InputValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Windows.UI.Xaml.Controls;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    public class ViewMap : IViewMap
    {
        private Dictionary<Type, Func<UserControl>> Views => _lazy.Value;
        private Lazy<Dictionary<Type, Func<UserControl>>> _lazy;

        public ViewMap(IList<object> map)
            : this(map.Cast<IPair>()
                      .ToList()
                      .As<IList<IPair>>()
              )
        {

        }

        public ViewMap(IList<Pair> map) : this(map.Cast<IPair>().ToList())
        {

        }

        public ViewMap(IList<IPair> map)
        {
            _lazy = new Lazy<Dictionary<Type, Func<UserControl>>>(
                () => new PairsAsDictionary(map).Value()
            );
        }

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


        public bool HasView(object viewModel)
        {
            return Views.ContainsKey(viewModel.GetType());
        }
    }
}
