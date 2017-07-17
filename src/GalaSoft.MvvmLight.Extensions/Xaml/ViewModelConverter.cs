using System;
using Windows.UI.Xaml.Data;
using InputValidation;
using Windows.UI.Xaml;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    public class ViewModelConverter : DependencyObject, IValueConverter
    {
        public ViewModelConverter()
        {

        }

        public ViewModelConverter(IViewMap map)
        {
            ViewMap = map;
        }

        public IViewMap ViewMap
        {
            get { return (IViewMap)GetValue(ViewMapProperty); }
            set { SetValue(ViewMapProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewMap.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewMapProperty =
            DependencyProperty.Register("ViewMap", typeof(IViewMap), typeof(ViewModelConverter), new PropertyMetadata(0));

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var map = ViewMap;
            map.CheckNotNull("Forget to set a ViewModel property in ViewModelConverter in Xaml code");
            return map.GetViewFor(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
