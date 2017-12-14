using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Markup;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    /// <summary>
    /// Allows to convert specific child name and <see cref="HostViewModel.Children"/> to view 
    /// by defining a mapping between view model and view.
    /// This is created from Xaml.
    /// </summary>
    [ContentProperty(Name = nameof(Map))]
    public class Mapping : ValueConverterGroup
    {
        /// <summary>
        /// The pair list or mapping.
        /// </summary>
        public IList<object> Map
        {
            get { return (IList<object>)GetValue(MapProperty); }
            set { SetValue(MapProperty, value); }
        }

        /// <summary>
        /// Map dependency property.
        /// </summary>
        public static readonly DependencyProperty MapProperty =
            DependencyProperty.Register("Map", typeof(IList<object>),
                typeof(Mapping), new PropertyMetadata(new List<object>()));

        /// <summary>
        /// Initializes a new instance of <see cref="Mapping"/>.
        /// To use with unit testing.
        /// </summary>
        /// <param name="map">The map.</param>
        public Mapping(List<Pair> map)
            : base(
                new ViewModelAccessor(),
                new ViewModelToViewConverter(
                    new ViewMap(map)
                )
              )
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="Mapping"/>.
        /// </summary>
        public Mapping()
        {
            _lazy = new System.Lazy<ValueConverterList>(
                        () => new ValueConverterList(
                                 new ViewModelAccessor(),
                                 new ViewModelToViewConverter(Map)
                              )
                    );
        }
    }
}
