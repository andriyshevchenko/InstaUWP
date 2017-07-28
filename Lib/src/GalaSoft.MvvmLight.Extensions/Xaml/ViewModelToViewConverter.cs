using System;
using Windows.UI.Xaml.Data;
using InputValidation;
using System.Collections.Generic;
using System.Linq;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    /// <summary>
    /// Converts a specific view model to view by using a <see cref="IViewMap"/>
    /// </summary>
    public class ViewModelToViewConverter : IValueConverter
    {
        private const string NoIViewMapDefinedInXaml 
            = "Forget to set a ViewMap property in ViewModelConverter.";

        /// <summary>
        /// Initializes a new instance of <see cref="ViewModelToViewConverter"/>.
        /// </summary>
        /// <param name="source">
        /// List of <see cref="Object"/>'s which is converter to List of <see cref="IPair"/>'s.
        /// If cast is not succesful, throws an <see cref="InvalidCastException"/>.
        /// </param>
        public ViewModelToViewConverter(IList<object> source) : this(source.Cast<IPair>().ToList())
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="ViewModelToViewConverter"/>.
        /// </summary>
        /// <param name="source">The pairs.</param>
        public ViewModelToViewConverter(IList<Pair> source) : this(source.Cast<IPair>().ToList())
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="ViewModelToViewConverter"/>.
        /// </summary>
        /// <param name="source">The pairs.</param>
        public ViewModelToViewConverter(IList<IPair> source) : this(new ViewMap(source))
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="ViewModelToViewConverter"/>.
        /// To use with unit testing.
        /// </summary>
        /// <param name="map">The ViewModel-View mapping.</param>
        public ViewModelToViewConverter(IViewMap map)
        {
            ViewMap = map;
        }

        /// <summary>
        /// The ViewModel-View mapping.
        /// </summary>
        public IViewMap ViewMap { get; internal set; }

        /// <summary>
        /// Converts a view model to view.
        /// </summary>
        /// <param name="value">The view model.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Converter parameter.</param>
        /// <param name="language">The culture.</param>
        /// <returns>New view instance for this view model.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ViewMap
                .CheckNotNull(NoIViewMapDefinedInXaml)
                .GetViewFor(value);
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
