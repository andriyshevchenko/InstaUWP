namespace GalaSoft.MvvmLight.Extensions
{
    /// <summary>
    /// A <see cref="IRedirectViewModel"/>, which is also a navigation root.
    /// </summary>
    public class HostingRedirectViewModel : ViewModelBase, INavigationRoot, IRedirectViewModel
    {
        private bool _notificationFlag = false;
        HostViewModel _root;
        IRedirectViewModel _source;

        /// <summary>
        ///  Initializes a new instance of <see cref="HostingRedirectViewModel"/> with default internal navigation logic.
        /// </summary>
        /// <param name="source"></param>
        public HostingRedirectViewModel(IRedirectViewModel source) : this(source, new HostViewModel())
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="HostingRedirectViewModel"/>.
        /// </summary>
        /// <param name="other">Other <see cref="IRedirectViewModel"/>.</param>
        /// <param name="root">The logic of internal navigation.</param>
        public HostingRedirectViewModel(IRedirectViewModel other, HostViewModel root) 
        {
            _source = other;
            _root = root;
            //Track a changes inside a navigation root.
            _root.PropertyChanged += 
                (sender, args) => Set(args.PropertyName, ref _notificationFlag, !_notificationFlag);
        }

        /// <summary>
        /// Determines if view model can navigate back.
        /// </summary>
        /// <param name="steps">The number of steps to make.</param>
        /// <returns>True if view model can navigate back.</returns>
        public bool CanGoBack(int steps = 1)
        {
            return _source.CanGoBack(steps);
        }

        /// <summary>
        /// Determines if specific child can navigate back.
        /// </summary>
        /// <param name="childName">The name of the child.</param>
        /// <param name="steps">The number of steps to make.</param>
        /// <returns>True if view model can navigate back.</returns>
        public bool CanGoBack(string childName, int steps = 1)
        {
            return _root.CanGoBack(childName, steps);
        }

        /// <summary>
        /// Determines if view model can navigate forward.
        /// </summary>
        /// <param name="steps">The number of steps to make.</param>
        /// <returns>True if view model can navigate forward.</returns>
        public bool CanGoForward(int steps = 1)
        {
            return _source.CanGoForward(steps);
        }

        /// <summary>
        /// Determines if specific child can navigate forward.
        /// </summary>
        /// <param name="childName">The name of the child.</param>
        /// <param name="steps">The number of steps to make.</param>
        /// <returns>True if view model can navigate forward.</returns>
        public bool CanGoForward(string childName, int steps = 1)
        {
            return _root.CanGoForward(childName, steps);
        }

        /// <summary>
        /// Navigates the current child back.
        /// </summary>
        /// <param name="steps">The number of steps to make.</param>
        public void GoBack(int steps = 1)
        {
            _source.GoBack(steps);
        }


        /// <summary>
        /// Navigates a specific child back.
        /// </summary>
        /// <param name="childName">The name of the child.</param>
        /// <param name="steps">The number of steps to make.</param>
        public void GoBack(string childName, int steps = 1)
        {
            _root.GoBack(childName, steps);
        }

        /// <summary>
        /// Navigates the current child forward.
        /// </summary>
        /// <param name="steps">The number of steps to make.</param>
        public void GoForward(int steps = 1)
        {
            _source.GoForward(steps);
        }

        /// <summary>
        /// Navigates a specific child forward.
        /// </summary>
        /// <param name="childName">The name of the child.</param>
        /// <param name="steps">The number of steps to make.</param>
        public void GoForward(string childName, int steps = 1)
        {
            _root.GoForward(childName, steps);
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
        /// Navigates a specific view by passing its mapped view model.
        /// </summary>
        /// <param name="childName">The name of the child.</param>
        /// <param name="viewModel">The view model.</param>
        public void NavigateTo(string childName, object viewModel)
        {
            _root.NavigateTo(childName, viewModel);
        }
    }
}
