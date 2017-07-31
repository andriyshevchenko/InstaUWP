using System;
using Windows.UI.Xaml.Data;

namespace GalaSoft.MvvmLight.Extensions.Test.Xaml
{
    public class FakeStringToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string source = value.As<string>();
            bool.TryParse(source, out bool result);
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
