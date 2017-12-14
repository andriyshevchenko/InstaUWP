using Cactoos;
using Cactoos.Reflection;
using Cactoos.Scalar;
using Cactoos.Text;
using System;
using System.Collections.Generic;
using System.Reflection;
using Windows.UI.Xaml;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    /// <summary>
    /// Pair which accepts a text representation of itself.
    /// </summary>
    public class Pair : DependencyObject, IPair
    {
        private IScalar<string> _correctViewTypeName;
        private IScalar<string> _correctViewModelTypeName;

        private static IScalar<Assembly> _app =
            new AssemblyOfType(
                Application.Current.GetType()
            );

        private static IScalar<Assembly> _lib =
            new AssemblyOfType<Pair>();

        private static AssemblyTypeCache _appTypeCache =
            new AssemblyTypeCache(_app);

        private static AssemblyTypeCache _libTypeCache =
            new AssemblyTypeCache(_lib);

        private static IScalar<IReadOnlyDictionary<string, Type>> _mergedTypeCache
            = new CachedScalar<IReadOnlyDictionary<string, Type>>(
                  new MergedTypeCache(_libTypeCache, _appTypeCache)
              );

        private static IScalar<IReadOnlyDictionary<string, Type[]>> _typeCacheWithoutNamespace
            = new CachedScalar<IReadOnlyDictionary<string, Type[]>>(
                  new SimpleNameTypeCache(
                      _mergedTypeCache
                  )
              );

        /// <summary>
        /// Initializes a new instance of <see cref="Pair"/>.
        /// </summary>
        public Pair()
        {
            _correctViewTypeName
               = new CachedScalar<string>(
                     new LazyScalar<string>(() =>
                         new InferredName(_typeCacheWithoutNamespace, View).String()
                     )
                 );
             
            _correctViewModelTypeName
               = new CachedScalar<string>(
                     new LazyScalar<string>(() =>
                         new InferredName(_typeCacheWithoutNamespace, ViewModel).String()
                     )
               );
        }

        /// <summary>
        /// The view type.
        /// </summary>
        public Type ViewType => _mergedTypeCache.Value()[_correctViewTypeName.Value()];

        /// <summary>
        /// The view model type.
        /// </summary>
        public Type ViewModelType => _mergedTypeCache.Value()[_correctViewModelTypeName.Value()];

        /// <summary>
        /// Text representation of view type.
        /// </summary>
        public string View
        {
            get { return (string)GetValue(ViewTypeNameProperty); }
            set { SetValue(ViewTypeNameProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for ViewTypeName.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty ViewTypeNameProperty =
            DependencyProperty.Register("View", typeof(string), typeof(Pair), new PropertyMetadata(null));

        /// <summary>
        /// Text representation of view model type.
        /// </summary>
        public string ViewModel
        {
            get { return (string)GetValue(ViewModelTypeNameProperty); }
            set { SetValue(ViewModelTypeNameProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for ViewModelTypeName.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty ViewModelTypeNameProperty =
            DependencyProperty.Register("ViewModel", typeof(string), typeof(Pair), new PropertyMetadata(null));
    }
}
