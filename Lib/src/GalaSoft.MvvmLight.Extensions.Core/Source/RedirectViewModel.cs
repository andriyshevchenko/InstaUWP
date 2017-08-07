using InputValidation;

namespace GalaSoft.MvvmLight.Extensions.Core
{
    /// <summary>
    /// View model which is able to replace itself with a new.
    /// It uses a parent <see cref="INavigationRoot"/> to navigate.
    /// </summary>
    public class RedirectViewModel : ViewModelBase, IRedirectViewModel, INavigationChild
    {
        /// <summary>
        /// The name of the main child. Used as a default parameter in many methods.
        /// </summary>
        public const string MainChild = "main";
        private INavigationRoot _root;
        private string _child;

        /// <summary>
        /// Initializes a new instance of <see cref="RedirectViewModel"/>.
        /// </summary>
        /// <param name="other">Other <see cref="RedirectViewModel"/>.</param>
        public RedirectViewModel(RedirectViewModel other) : this(other._root, other._child)
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="RedirectViewModel"/>.
        /// </summary>
        /// <param name="other">Other <see cref="INavigationChild"/>.</param>
        public RedirectViewModel(INavigationChild other) : this(other.Root, other.ChildName)
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="RedirectViewModel"/>.
        /// </summary>
        /// <param name="root">The <see cref="INavigationRoot"/>.</param>
        /// <param name="childName">The name of a child.</param>
        public RedirectViewModel(INavigationRoot root, string childName = MainChild)
        {
            _root = root.CheckNotNull("navigation root");
            _child = childName.CheckNotNull(nameof(childName));
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
        /// Navigates the current child forward.
        /// </summary>
        /// <param name="steps">The number of steps to make.</param>
        public void GoBack(int steps = 1)
        {
            _root.GoBack(_child, steps);
        }

        /// <summary>
        /// Navigates the current child back.
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
