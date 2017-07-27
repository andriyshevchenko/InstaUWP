using Cactoos;
using InputValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    public struct LazyView : IScalar<UserControl>
    {
        private object _viewModel;
        private Func<UserControl> _source;

        public LazyView(Func<UserControl> source, object viewModel)
        {
            _source = source;
            _viewModel = viewModel;
        }

        public UserControl Value()
        {
            var userControl = _source();
            userControl.DataContext = _viewModel;
            return userControl;
        }
    }

    public class ViewMap : IViewMap
    {
        private Dictionary<Type, Func<UserControl>> Views => _viewsLazy.Value;
        private Lazy<Dictionary<Type, Func<UserControl>>> _viewsLazy;

        public ViewMap(IList<object> map) 
            : this(map.Cast<IPair>()
                      .ToList()
                      .As<IList<IPair>>())
        {

        }

        public ViewMap(IList<IPair> map)
        {
            Map = map;
            _viewsLazy =
                new Lazy<Dictionary<Type, Func<UserControl>>>(
                    () => new MappedViews(Map).ToDictionary()
                );
        }

        public IList<IPair> Map { get; set; }

        public object GetViewFor(object viewModel)
        {
            Type type = viewModel.GetType();

            if (Views.ContainsKey(type))
            {
                return new LazyView(Views[type], viewModel).Value();
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
                    return new LazyView(Views[baseType], viewModel).Value();
                }

                //check if dictionary contains any of implemented
                var interfaces = type.GetInterfaces();
                if (interfaces.Any())
                {
                    for (int i = 0; i < interfaces.Length; i++)
                    {
                        Type iface = interfaces[i];
                        if (Views.ContainsKey(iface))
                        {
                            //redirect to implemented interface view
                            Views[type] = Views[iface];
                            return new LazyView(Views[iface], viewModel).Value();
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
