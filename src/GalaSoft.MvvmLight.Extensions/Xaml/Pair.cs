﻿using Cactoos;
using Cactoos.Scalar;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    /// <summary>
    /// Pair which accepts a text representation of itself.
    /// </summary>
    public class Pair : DependencyObject, IPair
    {
        private readonly string RootNamespace = $"{nameof(GalaSoft)}.{nameof(MvvmLight)}.{nameof(Extensions)}";

        private IScalar<string> _correctViewTypeName;
        private IScalar<string> _correctViewModelTypeName;
        
        private static IScalar<IReadOnlyDictionary<string, Type>> _typeCache
            = new CachedScalar<IReadOnlyDictionary<string, Type>>(
                  new MergedTypeCache(
                      new AssemblyOfType<Pair>(),
                      new AssemblyOfType(Application.Current.GetType())
                  )
              );

        /// <summary>
        /// Initializes a new instance of <see cref="Pair"/>.
        /// </summary>
        public Pair()
        {
            _correctViewTypeName
                = new LazyScalar<string>(() =>
                      new NamespacedName(ViewTypeName, RootNamespace).String()
                  );

            _correctViewModelTypeName
                = new LazyScalar<string>(() =>
                      new NamespacedName(ViewModelTypeName, RootNamespace).String()
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
