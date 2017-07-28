namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    /// <summary>
    /// Defines a mapping between a view models and views.
    /// </summary>
    public interface IViewMap
    {
        /// <summary>
        /// Determines if type of view model has a corresponding view.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>True if specific type of view model has view defined.</returns>
        bool HasView(object viewModel);

        /// <summary>
        /// Gets a new view instance for specific view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>New view instance.</returns>
        object GetViewFor(object viewModel);
    }
}
