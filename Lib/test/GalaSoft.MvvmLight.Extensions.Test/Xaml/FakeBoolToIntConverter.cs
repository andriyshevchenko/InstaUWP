using System;
using Windows.UI.Xaml.Data;

namespace GalaSoft.MvvmLight.Extensions.Test.Xaml
{
    public class FakeBoolToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value.NotNull())
            {
                var source = (bool)value;
                return source ? 1 : 0;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
