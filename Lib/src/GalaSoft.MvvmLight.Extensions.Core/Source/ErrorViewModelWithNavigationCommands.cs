using System;
using System.Windows.Input;

namespace GalaSoft.MvvmLight.Extensions
{
    /// <summary>
    /// Error view model, which support a navigation commands to use with data binding.
    /// </summary>
    public class ErrorViewModelWithNavigationCommands : IErrorViewModel, ISimpleNavigationViewModel
    {
        private ISimpleNavigationViewModel _source;
        private IErrorViewModel _error;

        /// <summary>
        /// Initializes a new instance of <see cref="ErrorViewModelWithNavigationCommands"/>.
        /// </summary>
        /// <param name="source">Source <see cref="IErrorViewModel"/>.</param>
        public ErrorViewModelWithNavigationCommands(IErrorViewModel source)
        {
            _error = source;
            _source = new ViewModelWithNavigationCommands(source);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ErrorViewModelWithNavigationCommands"/>.
        /// </summary>
        /// <param name="root">The parent.</param>
        /// <param name="error">An error.</param>
        public ErrorViewModelWithNavigationCommands(INavigationRoot root, Exception error): this(new ErrorViewModel(root, error))
        {

        }

        /// <summary>
        /// An <see cref="ICommand"/> to navigate back.
        /// </summary>
        public ICommand GoForwardCommand => _source.GoForwardCommand;

        /// <summary>
        /// An <see cref="ICommand"/> to navigate forward.
        /// </summary>
        public ICommand GoBackCommand => _source.GoBackCommand;

        /// <summary>
        /// The error message.
        /// </summary>
        public string Error => _error.Error;

        /// <summary>
        /// Determines if view model can navigate back.
        /// </summary>
        /// <param name="steps">The number of steps to make.</param>
        /// <returns>True if view model can navigate back.</returns>
        public bool CanGoBack(int steps = 1)
        {
            return _error.CanGoBack(steps);
        }

        /// <summary>
        /// Determines if view model can navigate forward.
        /// </summary>
        /// <param name="steps">The number of steps to make.</param>
        /// <returns>True if view model can navigate forward.</returns>
        public bool CanGoForward(int steps = 1)
        {
            return _error.CanGoForward(steps);
        }

        /// <summary>
        /// Navigates the current child forward.
        /// </summary>
        /// <param name="steps">The number of steps to make.</param>
        public void GoBack(int steps = 1)
        {
            _error.GoBack(steps);
        }

        /// <summary>
        /// Navigates the current child back.
        /// </summary>
        /// <param name="steps">The number of steps to make.</param>
        public void GoForward(int steps = 1)
        {
            _error.GoForward(steps);
        }

        /// <summary>
        /// Navigates the current child to a new view model.
        /// </summary>
        /// <param name="viewModel"></param>
        public void NavigateTo(object viewModel)
        {
            _error.NavigateTo(viewModel);
        }
    }
}
