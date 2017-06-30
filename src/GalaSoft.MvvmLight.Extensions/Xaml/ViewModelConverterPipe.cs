using System;
using Windows.UI.Xaml;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    public class ViewModelConverterPipe : ValueConverterPipe
    {
        public ViewMapItemCollection Map
        {
            get { return (ViewMapItemCollection)GetValue(MapProperty); }
            set { SetValue(MapProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Map.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MapProperty =
            DependencyProperty.Register("Map", typeof(ViewMapItemCollection), typeof(ViewMap), new PropertyMetadata(0));

        public ViewModelConverterPipe()
        {
            Converters = new ValueConverterCollection
            {
                new ViewModelAccessor(),
                new ViewModelConverter(
                    new ViewMap(
                        new ViewMapItemCollection(Map ?? throw new InvalidOperationException()
                        )
                    )
                )
            };
        }
    }
}
