namespace GalaSoft.MvvmLight.Extensions
{
    /// <summary>
    /// View model which is able to replace itself with a new.
    /// </summary>
    public interface IRedirectViewModel
    {
        /// <summary>
        /// Determines if view model can navigate back.
        /// </summary>
        /// <param name="steps">The number of steps to make.</param>
        /// <returns>True if view model can navigate back.</returns>
        bool CanGoBack(int steps = 1);

        /// <summary>
        /// Determines if view model can navigate forward.
        /// </summary>
        /// <param name="steps">The number of steps to make.</param>
        /// <returns>True if view model can navigate forward.</returns>
        bool CanGoForward(int steps = 1);

        /// <summary>
        /// Navigates the current child forward.
        /// </summary>
        /// <param name="steps">The number of steps to make.</param>
        void GoForward(int steps = 1);

        /// <summary>
        /// Navigates the current child back.
        /// </summary>
        /// <param name="steps">The number of steps to make.</param>
        void GoBack(int steps = 1);

        /// <summary>
        /// Navigates the current child to a new view model.
        /// </summary>
        /// <param name="viewModel"></param>
        void NavigateTo(object viewModel);
    }
}
