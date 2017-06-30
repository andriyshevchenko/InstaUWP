using InputValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using static System.Functional.Func;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    public class ViewMap : DependencyObject, IViewMap
    {
        public ViewMap()
        {

        }
        public ViewMap(ViewMapItemCollection map)
        {
            Map = map;
        }

        public ViewMapItemCollection Map
        {
            get { return (ViewMapItemCollection)GetValue(MapProperty); }
            set { SetValue(MapProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Map.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MapProperty =
            DependencyProperty.Register("Map", typeof(ViewMapItemCollection), typeof(ViewMap), new PropertyMetadata(0));
         
        public object GetViewFor(object viewModel)
        {
            _views = Lazy(ref _views, () => new MappedViews(Map).ToDictionary());
            Type type = viewModel.GetType();

            if (_views.ContainsKey(type))
            {
                var func = _views[type];
                return monad(func().As<FrameworkElement>(), 
                             element => element.DataContext = viewModel);
            }

            throw new InvalidOperationException($"View for requested view model {type} not added yet");
        }

        private Dictionary<Type, Func<UserControl>> _views;

        private static T Lazy<T>(ref T item, Func<T> @return) where T:class
        {
            return item ?? (item = @return());
        }

        public bool HasView(object viewModel)
        {
            return _views.ContainsKey(viewModel.GetType());
        }
    }
}
