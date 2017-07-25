using InputValidation;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using static System.Functional.Func;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    public class ViewMap : IViewMap
    {
        public ViewMap(PairList map)
        {
            Map = map;
            _viewsLazy =
                new Lazy<Dictionary<Type, Func<UserControl>>>(
                    () => new MappedViews(Map).ToDictionary()
                );
        }

        public PairList Map { get; set; }
        
        public object GetViewFor(object viewModel)
        {
            Type type = viewModel.GetType();

            if (Views.ContainsKey(type))
            {
                return monad(
                           Views[type]().As<FrameworkElement>(), 
                           element => element.DataContext = viewModel
                       );
            }

            throw new InvalidOperationException($"View for requested view model {type} not added yet");
        }

        private Dictionary<Type, Func<UserControl>> Views => _viewsLazy.Value;
        private Lazy<Dictionary<Type, Func<UserControl>>> _viewsLazy;

        public bool HasView(object viewModel)
        {
            return Views.ContainsKey(viewModel.GetType());
        }
    }
}
