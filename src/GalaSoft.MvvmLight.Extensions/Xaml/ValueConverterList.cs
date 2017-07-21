using System.Collections.Generic;
using Windows.UI.Xaml.Data;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    public class ValueConverterList : List<IValueConverter>
    {
        public ValueConverterList(params IValueConverter[] source) : base(source)
        {

        }
    }
}
