using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Markup;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    [ContentProperty(Name = nameof(Map))]
    public class ViewModelConverterPipe : ValueConverterGroup
    {
        public IList<object> Map
        {
            get { return (IList<object>)GetValue(MapProperty); }
            set
            {
                SetValue(MapProperty, value);
            }
        }

        public static readonly DependencyProperty MapProperty =
            DependencyProperty.Register("Map", typeof(IList<object>),
                typeof(ViewModelConverterPipe), new PropertyMetadata(new List<object>()));

        public ViewModelConverterPipe(List<Pair> map)
            : base(
                new ViewModelAccessor(),
                new ViewModelToViewConverter(new ViewMap(map.Cast<IPair>().ToList()))
              )
        {

        }

        public ViewModelConverterPipe()
        {
            _lazy = new System.Lazy<ValueConverterList>(
                        () => new ValueConverterList(
                                 new ViewModelAccessor(),
                                 new ViewModelToViewConverter(
                                     Map.Cast<IPair>().ToList()
                                 )
                              )
                    );
        }
    }
}
