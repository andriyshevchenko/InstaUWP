using System;
using Windows.UI.Xaml.Data;
using InputValidation;
using System.Collections.Generic;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    public class ViewModelAccessor : IValueConverter
    {
        public ViewModelAccessor()
        {

        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var children = value.As<IReadOnlyDictionary<string, object>>();
            var childName = parameter.As<string>();
            if (children.ContainsKey(childName))
            {
                return children[childName];
            }
            throw new KeyNotFoundException(nameof(childName));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
