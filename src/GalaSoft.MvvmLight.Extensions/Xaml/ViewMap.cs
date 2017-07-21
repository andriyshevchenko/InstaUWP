using InputValidation;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using static System.Functional.Func;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    public class ViewMap : DependencyObject, IViewMap
    {
        public ViewMap()
        {
            _viewsLazy = new Lazy<Dictionary<Type, Func<UserControl>>>(() => new MappedViews(Map).ToDictionary());
        }
        public ViewMap(PairCollection map)
        {
            Map = map;
        }

        public PairCollection Map
        {
            get { return (PairCollection)GetValue(MapProperty); }
            set { SetValue(MapProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Map.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MapProperty =
            DependencyProperty.Register("Map", typeof(PairCollection), typeof(ViewMap), new PropertyMetadata(0));
         
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
