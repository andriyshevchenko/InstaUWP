using System;
using Windows.UI.Xaml.Data;
using InputValidation;
using System.Collections.Generic;
using Windows.UI.Xaml;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    /// <summary>
    /// Allows to access a specific view model of <see cref="HostViewModel.Children"/>.
    /// </summary>
    public class ViewModelAccessor : IValueConverter
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ViewModelAccessor"/>.
        /// </summary>
        public ViewModelAccessor()
        {

        }

        /// <summary>
        /// Accesses a specific view model of <see cref="HostViewModel.Children"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">The type.</param>
        /// <param name="parameter">The child name.</param>
        /// <param name="language">The culture.</param>
        /// <returns>New value.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var children = value.As<IReadOnlyDictionary<string, object>>();
            var childName = parameter.As<string>();
            if (children.ContainsKey(childName))
            {
                return children[childName];
            }
            return DependencyProperty.UnsetValue;
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
