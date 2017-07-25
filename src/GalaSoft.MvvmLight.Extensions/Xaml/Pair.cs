using System;
using System.Diagnostics;
using System.Reflection;
using Windows.UI.Xaml;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    /// <summary>
    /// Represents pair View-ViewModel
    /// </summary>
    public class Pair : DependencyObject, IPair
    {
        public Pair()
        {
            var m = Application.Current.GetType().GetTypeInfo().Assembly.FullName;
        }
        public Pair(Type view, Type viewModel)
        {
            View = view;
            ViewModel = viewModel;
        }

        /// <summary>
        /// View dependency property
        /// </summary>
        public Type View
        {
            get { return (Type)GetValue(ViewProperty); }
            set
            {
                SetValue(ViewProperty, value);
                var tr = new StackTrace(new Exception(), false);
            }
        }

        // Using a DependencyProperty as the backing store for View.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewProperty =
            DependencyProperty.Register("View", typeof(Type), typeof(Pair), new PropertyMetadata(null));

        /// <summary>
        /// ViewModel dependency property
        /// </summary>
        public Type ViewModel
        {
            get { return (Type)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(Type), typeof(Pair), new PropertyMetadata(null));
    }
}
