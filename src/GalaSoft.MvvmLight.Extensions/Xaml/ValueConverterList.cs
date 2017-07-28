using System.Collections.Generic;
using Windows.UI.Xaml.Data;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    /// <summary>
    /// List of <see cref="IValueConverter"/>.
    /// </summary>
    public class ValueConverterList : List<IValueConverter>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ValueConverterList"/>.
        /// </summary>
        /// <param name="source">Converters.</param>
        public ValueConverterList(params IValueConverter[] source) : base(source)
        {

        }
    }
}
