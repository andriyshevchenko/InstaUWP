using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using InputValidation;
using static System.Collections.Generic.Create;
using static System.Functional.Func;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    public class ViewMapItem : DependencyObject
    {
        public Type View
        {
            get { return (Type)GetValue(ViewProperty); }
            set { SetValue(ViewProperty, value); }
        }

        // Using a DependencyProperty as the backing store for View.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewProperty =
            DependencyProperty.Register("View", typeof(Type), typeof(ViewMapItem), new PropertyMetadata(0));


        public Type ViewModel
        {
            get { return (Type)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(Type), typeof(ViewMapItem), new PropertyMetadata(0));
    }

    public class ViewMapItemCollection : System.Collections.Generic.List<ViewMapItem>
    {

    }

    public class ViewMap : DependencyObject, IViewMap
    {
        public ViewMap()
        {
        }

        public ViewMapItemCollection Map
        {
            get { return (ViewMapItemCollection)GetValue(MapProperty); }
            set { SetValue(MapProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Map.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MapProperty =
            DependencyProperty.Register("Map", typeof(ViewMapItemCollection), typeof(ViewMap), new PropertyMetadata(0));
         
        class MappedViews
        {
            ViewMapItemCollection _items;
            public Dictionary<Type, Func<Page>> ToDictionary()
            {
                return dictionary(_items.Select(item => (item.ViewModel, fun(() => Activator.CreateInstance(item.View).As<Page>()))));
            }
            public MappedViews(ViewMapItemCollection items)
            {
                _items = items;
            }
        }

        public object GetViewFor(object viewModel)
        {
            _views = Lazy(ref _views, () => new MappedViews(Map).ToDictionary());
            Type type = viewModel.GetType();
            if (_views.ContainsKey(type))
            {
                return _views[type];
            }
            throw new InvalidOperationException($"View for view model {type} not added yet");
        }

        private Dictionary<Type, Func<Page>> _views;

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
