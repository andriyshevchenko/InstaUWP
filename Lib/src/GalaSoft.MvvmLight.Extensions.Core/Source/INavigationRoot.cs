namespace GalaSoft.MvvmLight.Extensions
{
    /// <summary>
    /// Defines a root of navigation.
    /// </summary>
    public interface INavigationRoot
    {
        /// <summary>
        /// Determines if specific child can navigate back.
        /// </summary>
        /// <param name="childName">The name of the child.</param>
        /// <param name="steps">The number of steps to make.</param>
        /// <returns>True if view model can navigate back.</returns>
        bool CanGoBack(string childName, int steps = 1);

        /// <summary>
        /// Determines if specific child can navigate forward.
        /// </summary>
        /// <param name="childName">The name of the child.</param>
        /// <param name="steps">The number of steps to make.</param>
        /// <returns>True if view model can navigate forward.</returns>
        bool CanGoForward(string childName, int steps = 1);

        /// <summary>
        /// Navigates a specific child forward.
        /// </summary>
        /// <param name="childName">The name of the child.</param>
        /// <param name="steps">The number of steps to make.</param>
        void GoForward(string childName, int steps = 1);

        /// <summary>
        /// Navigates a specific child back.
        /// </summary>
        /// <param name="childName">The name of the child.</param>
        /// <param name="steps">The number of steps to make.</param>
        void GoBack(string childName, int steps = 1);

        /// <summary>
        /// Navigates a specific view by passing its mapped view model.
        /// </summary>
        /// <param name="childName">The name of the child.</param>
        /// <param name="viewModel">The view model.</param>
        void NavigateTo(string childName, object viewModel);
    }
}
