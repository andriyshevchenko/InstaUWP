namespace GalaSoft.MvvmLight.Extensions.Core
{
    /// <summary>
    /// A <see cref="IRedirectViewModel"/>, which is also a navigation root.
    /// </summary>
    public class RedirectViewModelWithContent : HostViewModel, IRedirectViewModel, INavigationChild
    {
        private INavigationRoot _root;
        private string _child;

        /// <summary>
        /// Initializes a new instance of <see cref="RedirectViewModelWithContent"/> with default internal navigation logic.
        /// </summary>  
        /// <param name="source">The <see cref="IRedirectViewModel"/>.</param>
        public RedirectViewModelWithContent(INavigationChild source) : this(source.Root, source.ChildName)
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="RedirectViewModelWithContent"/>.
        /// </summary>
        /// <param name="root">The navigation root.</param>
        /// <param name="childName">The name of the child.</param>
        public RedirectViewModelWithContent(INavigationRoot root, string childName)
        {
            _root = root;
            _child = childName;
        }

        /// <summary>
        /// Determines if view model can navigate back.
        /// </summary>
        /// <param name="steps">The number of steps to make.</param>
        /// <returns>True if view model can navigate back.</returns>
        public bool CanGoBack(int steps = 1)
        {
            return _root.CanGoBack(_child, steps);
        }

        /// <summary>
        /// Determines if view model can navigate forward.
        /// </summary>
        /// <param name="steps">The number of steps to make.</param>
        /// <returns>True if view model can navigate forward.</returns>
        public bool CanGoForward(int steps = 1)
        {
            return _root.CanGoForward(_child, steps);
        }

        /// <summary>
        /// Navigates the current child back.
        /// </summary>
        /// <param name="steps">The number of steps to make.</param>
        public void GoBack(int steps = 1)
        {
            _root.GoBack(_child, steps);
        }

        /// <summary>
        /// Navigates the current child forward.
        /// </summary>
        /// <param name="steps">The number of steps to make.</param>
        public void GoForward(int steps = 1)
        {
            _root.GoForward(_child, steps);
        }

        /// <summary>
        /// Navigates the current child to a new view model.
        /// </summary>
        /// <param name="viewModel"></param>
        public void NavigateTo(object viewModel)
        {
            _root.NavigateTo(_child, viewModel);
        }

        /// <summary>
        /// The navigation root.
        /// </summary>
        public INavigationRoot Root => _root;

        /// <summary>
        /// The name of the child.
        /// </summary>
        public string ChildName => _child;
    }
}
