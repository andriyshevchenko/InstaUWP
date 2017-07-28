using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    /// <summary>
    /// Allows to pipeline the multiple converters.
    /// </summary>
    public class ValueConverterGroup : DependencyObject, IValueConverter
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ValueConverterGroup"/>.
        /// </summary>
        /// <param name="source">Converters.</param>
        public ValueConverterGroup(params IValueConverter[] source)
        {
            _lazy = new Lazy<ValueConverterList>(() => new ValueConverterList(source));
        }

        /// <summary>
        /// Lazy converters list.
        /// </summary>
        protected Lazy<ValueConverterList> _lazy;

        /// <summary>
        /// Gets the converters.
        /// </summary>
        public ValueConverterList Converters => _lazy.Value;

        /// <summary>
        /// Converts the given value chaining the converters.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">The type.</param>
        /// <param name="parameter">Converter parameter.</param>
        /// <param name="language">The culture.</param>
        /// <returns>New value.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            object returnValue = value;
            foreach (var item in Converters)
            {
                returnValue = item.Convert(returnValue, targetType, parameter, language);
            }
            return returnValue;
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        /// <param name="value">Not supported.</param>
        /// <param name="targetType">Not supported.</param>
        /// <param name="parameter">Not supported.</param>
        /// <param name="language">Not supported.</param>
        /// <returns>Not supported.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
