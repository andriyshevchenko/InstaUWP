using Cactoos;
using System;
using Windows.UI.Xaml.Controls;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    /// <summary>
    /// Lazy loading factory for <see cref="UserControl"/>.
    /// </summary>
    public struct LazyUserControl : IScalar<UserControl>
    {
        private object _viewModel;
        private Func<UserControl> _source;

        /// <summary>
        /// Initializes a new instance of <see cref="LazyUserControl"/>.
        /// </summary>
        /// <param name="source">Function to create a new <see cref="UserControl"/>.</param>
        /// <param name="viewModel">The view model.</param>
        public LazyUserControl(Func<UserControl> source, object viewModel)
        {
            _source = source;
            _viewModel = viewModel;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns>New <see cref="UserControl"/>.</returns>
        public UserControl Value()
        {
            var userControl = _source();
            userControl.DataContext = _viewModel;
            return userControl;
        }
    }
}
