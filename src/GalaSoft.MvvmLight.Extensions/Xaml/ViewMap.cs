using InputValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
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
                FrameworkElement view = Views[type].Invoke();
                view.DataContext = viewModel;
                return view;
            }

            throw new InvalidOperationException($"View for requested view model {type} not added yet");
        }


        public bool HasView(object viewModel)
        {
            return Views.ContainsKey(viewModel.GetType());
        }
    }
}
