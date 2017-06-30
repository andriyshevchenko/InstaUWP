using System;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    public class ValueConverterPipe : DependencyObject, IValueConverter
    {
        public ValueConverterCollection Converters
        {
            get { return (ValueConverterCollection)GetValue(ConvertersProperty); }
            set { SetValue(ConvertersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Converters.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConvertersProperty =
            DependencyProperty.Register("Converters", typeof(ValueConverterCollection), typeof(ValueConverterPipe), new PropertyMetadata(0));

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            object returnValue = value;
            foreach (var item in Converters ?? new ValueConverterCollection())
            {
                returnValue = item.Convert(returnValue, targetType, parameter, language);
            }
            return returnValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
