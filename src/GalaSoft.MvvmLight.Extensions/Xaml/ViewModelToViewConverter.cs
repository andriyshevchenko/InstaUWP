﻿using System;
using Windows.UI.Xaml.Data;
using InputValidation;
using Windows.UI.Xaml;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    public class ViewModelToViewConverter : DependencyObject, IValueConverter
    {
        public ViewModelToViewConverter()
        {

        }

        public ViewModelToViewConverter(IViewMap map)
        {
            ViewMap = map;
        }

        public IViewMap ViewMap
        {
            get { return (IViewMap)GetValue(ViewMapProperty); }
            set { SetValue(ViewMapProperty, value); }
        }

        private const string NoIViewMapDefinedInXaml = "Forget to set a ViewMap property in ViewModelConverter in Xaml code";

        // Using a DependencyProperty as the backing store for ViewMap.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewMapProperty =
            DependencyProperty.Register("ViewMap", typeof(IViewMap), typeof(ViewModelToViewConverter), new PropertyMetadata(0));

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