using Cactoos;
using Cactoos.Scalar;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    public class Pair : DependencyObject, IPair
    {
        private static IScalar<IReadOnlyDictionary<string, Type>> _typeCache
            = new CachedScalar<IReadOnlyDictionary<string, Type>>(
                  new MergedTypeCache(
                      new AssemblyTypeCache(
                          new AssemblyOfType<Pair>() 
                      ),
                      new AssemblyTypeCache(
                          new AssemblyOfType(Application.Current.GetType())
                      )
                  )
              );
        
        public Pair()
        {

        }

        public Type View => _typeCache.Value()[ViewTypeName];
        public Type ViewModel => _typeCache.Value()[ViewModelTypeName];

        public string ViewTypeName
        {
            get { return (string)GetValue(ViewTypeNameProperty); }
            set { SetValue(ViewTypeNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewTypeName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewTypeNameProperty =
            DependencyProperty.Register("ViewTypeName", typeof(string), typeof(Pair), new PropertyMetadata(null));

        public string ViewModelTypeName
        {
            get { return (string)GetValue(ViewModelTypeNameProperty); }
            set { SetValue(ViewModelTypeNameProperty, value); }
        }
        
        // Using a DependencyProperty as the backing store for ViewModelTypeName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewModelTypeNameProperty =
            DependencyProperty.Register("ViewModelTypeName", typeof(string), typeof(Pair), new PropertyMetadata(null));
    }
}
