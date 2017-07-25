using System;
using Windows.UI.Xaml.Data;
using InputValidation;
using Windows.UI.Xaml;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    public class ViewModelToViewConverter : IValueConverter
    {
        public ViewModelToViewConverter()
        {

        }

        public ViewModelToViewConverter(IViewMap map)
        {
            ViewMap = map;
        }

        public IViewMap ViewMap { get; set; }

        private const string NoIViewMapDefinedInXaml = "Forget to set a ViewMap property in ViewModelConverter in Xaml code";

        // Using a DependencyProperty as the backing store for ViewMap.  This enables animation, styling, binding, etc...
      
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ViewMap
                .CheckNotNull(NoIViewMapDefinedInXaml)
                .GetViewFor(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
