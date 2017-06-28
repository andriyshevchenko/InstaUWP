using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    public class ValueConverterPipe : DependencyObject, IValueConverter
    {
        public IValueConverter[] Converters
        {
            get { return (IValueConverter[])GetValue(ConvertersProperty); }
            set { SetValue(ConvertersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Converters.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConvertersProperty =
            DependencyProperty.Register("Converters", typeof(IValueConverter[]), typeof(ValueConverterPipe), new PropertyMetadata(0));

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            object returnValue = value;
            foreach (var item in Converters)
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
