using Windows.UI.Xaml;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    public class ViewModelConverterPipe : ValueConverterGroup
    {
        public PairList Map
        {
            get { return (PairList)GetValue(MapProperty); }
            set { SetValue(MapProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Map.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MapProperty =
            DependencyProperty.Register("Map", typeof(PairList), typeof(ViewMap), new PropertyMetadata(null));

        public ViewModelConverterPipe(PairList map) 
            : base(
                new ViewModelAccessor(),
                new ViewModelToViewConverter(
                    new ViewMap(map)
                ))
        {

        }

        public ViewModelConverterPipe()
        {

        }
    }
}
