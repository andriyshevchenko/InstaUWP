using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    public class ValueConverterGroup : DependencyObject, IValueConverter
    {
        public ValueConverterGroup()
        {

        }

        public ValueConverterGroup(params IValueConverter[] source)
        {
            _lazy = new Lazy<ValueConverterList>(() => new ValueConverterList(source));
        }

        protected Lazy<ValueConverterList> _lazy;
        public ValueConverterList Converters => _lazy.Value;

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
