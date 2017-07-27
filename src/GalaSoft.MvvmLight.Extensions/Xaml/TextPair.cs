using System;
using System.Reflection;
using Windows.UI.Xaml;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    public class TextPair : DependencyObject, IPair
    {   
        private static AssemblyTypeCache _typeCache
            = new AssemblyTypeCache(Application.Current.GetType().GetTypeInfo().Assembly);

        public TextPair()
        {

        }

        public Type View => _typeCache.Values()[ViewTypeName];
        public Type ViewModel => _typeCache.Values()[ViewModelTypeName];

        public string ViewTypeName
        {
            get { return (string)GetValue(ViewTypeNameProperty); }
            set { SetValue(ViewTypeNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewTypeName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewTypeNameProperty =
            DependencyProperty.Register("ViewTypeName", typeof(string), typeof(TextPair), new PropertyMetadata(null));

        public string ViewModelTypeName
        {
            get { return (string)GetValue(ViewModelTypeNameProperty); }
            set { SetValue(ViewModelTypeNameProperty, value); }
        }
        
        // Using a DependencyProperty as the backing store for ViewModelTypeName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewModelTypeNameProperty =
            DependencyProperty.Register("ViewModelTypeName", typeof(string), typeof(TextPair), new PropertyMetadata(null));
    }
}
