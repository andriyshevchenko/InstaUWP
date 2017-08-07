namespace GalaSoft.MvvmLight.Extensions.Core
{
    /// <summary>
    /// A <see cref="IRedirectViewModel"/>, which is also a navigation root.
    /// </summary>
    public class RedirectViewModelWithContent : ViewModelBase, INavigationRoot, IRedirectViewModel, INavigationChild
    {
        private bool _notificationFlag = false;
        private HostViewModel _logic;
        private INavigationRoot _root;
        private string _child;

        /// <summary>
        ///  Initializes a new instance of <see cref="RedirectViewModelWithContent"/> with default internal navigation logic.
        /// </summary>
        /// <param name="root">The <see cref="INavigationRoot"/>.</param>
        /// <param name="childName">The name of a child.</param>
        public RedirectViewModelWithContent(INavigationRoot root, string childName) : this(new RedirectViewModel(root, childName))
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="RedirectViewModelWithContent"/> with default internal navigation logic.
        /// </summary>  
        /// <param name="source">The <see cref="IRedirectViewModel"/>.</param>
        public RedirectViewModelWithContent(INavigationChild source) : this(source.Root, source.ChildName, new HostViewModel())
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="RedirectViewModelWithContent"/>.
        /// </summary>
        /// <param name="root">The navigation root.</param>
        /// <param name="childName">The name of the child.</param>
        /// <param name="logic">The logic of internal navigation.</param>
        public RedirectViewModelWithContent(INavigationRoot root, string childName, HostViewModel logic)
        {
            _root = root;
            _child = childName;
            //Track a changes inside aggregated logic.
            _logic = logic;
            _logic.PropertyChanged +=
                (sender, args) => Set(args.PropertyName, ref _notificationFlag, !_notificationFlag);
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
        /// Determines if specific internal child can navigate back.
        /// </summary>
        /// <param name="childName">The name of the child.</param>
        /// <param name="steps">The number of steps to make.</param>
        /// <returns>True if view model can navigate back.</returns>
        public bool CanGoBack(string childName, int steps = 1)
        {
            return _logic.CanGoBack(childName, steps);
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
        /// Determines if specific internal child can navigate forward.
        /// </summary>
        /// <param name="childName">The name of the child.</param>
        /// <param name="steps">The number of steps to make.</param>
        /// <returns>True if view model can navigate forward.</returns>
        public bool CanGoForward(string childName, int steps = 1)
        {
            return _logic.CanGoForward(childName, steps);
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
        /// Navigates a specific internal child back.
        /// </summary>
        /// <param name="childName">The name of the child.</param>
        /// <param name="steps">The number of steps to make.</param>
        public void GoBack(string childName, int steps = 1)
        {
            _logic.GoBack(childName, steps);
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
        /// Navigates a specific internal child forward.
        /// </summary>
        /// <param name="childName">The name of the child.</param>
        /// <param name="steps">The number of steps to make.</param>
        public void GoForward(string childName, int steps = 1)
        {
            _logic.GoForward(childName, steps);
        }

        /// <summary>
        /// Navigates the current child to a new view model.
        /// </summary>
        /// <param name="viewModel"></param>
        public void NavigateTo(object viewModel)
        {
            NavigateTo(viewModel);
        }

        /// <summary>
        /// Navigates a specific internal view by passing its mapped view model.
        /// </summary>
        /// <param name="childName">The name of the child.</param>
        /// <param name="viewModel">The view model.</param>
        public void NavigateTo(string childName, object viewModel)
        {
            _logic.NavigateTo(childName, viewModel);
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
