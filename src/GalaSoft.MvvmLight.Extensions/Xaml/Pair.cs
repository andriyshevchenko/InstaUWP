using Cactoos;
using Cactoos.Scalar;
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
        private readonly string CurrentRootNamespace = $"{nameof(GalaSoft)}.{nameof(MvvmLight)}.{nameof(Extensions)}";

        private IScalar<string> _correctViewTypeName;
        private IScalar<string> _correctViewModelTypeName;

        private static IScalar<string> _appRootNamespace =
            new CachedScalar<string>(
                new AssemblyRootNamespace(_app)
            );

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

        private static IScalar<IReadOnlyDictionary<string, Type>> _typeCache
            = new CachedScalar<IReadOnlyDictionary<string, Type>>(
                  new MergedTypeCache(_libTypeCache, _appTypeCache)
              );

        /// <summary>
        /// Initializes a new instance of <see cref="Pair"/>.
        /// </summary>
        public Pair()
        {
            _correctViewTypeName
               = new CachedScalar<string>(
                     new LazyScalar<string>(() =>
                         new InferredName(
                             ViewTypeName,
                             (
                                 new AssemblyRootNamespace(_app),
                                 new AssemblyTypeCache(_app)
                             ),
                             (
                                 new AssemblyRootNamespace(_lib),
                                 new AssemblyTypeCache(_lib)
                             )
                         ).Value()
                     )
                );

            _correctViewModelTypeName
                 = new CachedScalar<string>(
                     new LazyScalar<string>(() =>
                         new InferredName(
                             ViewModelTypeName,
                             (
                                 new AssemblyRootNamespace(_app),
                                 new AssemblyTypeCache(_app)
                             ),
                             (
                                 new AssemblyRootNamespace(_lib),
                                 new AssemblyTypeCache(_lib)
                             )
                         ).Value()
                     )
                );
        }

        /// <summary>
        /// The view type.
        /// </summary>
        public Type View => _typeCache.Value()[_correctViewTypeName.Value()];

        /// <summary>
        /// The view model type.
        /// </summary>
        public Type ViewModel => _typeCache.Value()[_correctViewModelTypeName.Value()];

        /// <summary>
        /// Text representation of view type.
        /// </summary>
        public string ViewTypeName
        {
            get { return (string)GetValue(ViewTypeNameProperty); }
            set { SetValue(ViewTypeNameProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for ViewTypeName.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty ViewTypeNameProperty =
            DependencyProperty.Register("ViewTypeName", typeof(string), typeof(Pair), new PropertyMetadata(null));

        /// <summary>
        /// Text representation of view model type.
        /// </summary>
        public string ViewModelTypeName
        {
            get { return (string)GetValue(ViewModelTypeNameProperty); }
            set { SetValue(ViewModelTypeNameProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for ViewModelTypeName.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty ViewModelTypeNameProperty =
            DependencyProperty.Register("ViewModelTypeName", typeof(string), typeof(Pair), new PropertyMetadata(null));
    }
}
