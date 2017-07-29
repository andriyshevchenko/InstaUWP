using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace GalaSoft.MvvmLight.Extensions
{
    /// <summary>
    /// View model with <see cref="GoBackCommand"/> and <see cref="GoForwardCommand"/>.
    /// </summary>
    public class ViewModelWithNavigationCommands : IRedirectViewModel, ISimpleNavigationViewModel
    {
        private IRedirectViewModel _source;

        /// <summary>
        /// An <see cref="ICommand"/> to navigate forward in navigation stack.
        /// </summary>
        public ICommand GoForwardCommand =>
            new RelayCommand(() =>
            {
                if (CanGoForward())
                {
                    GoForward();
                }
            });

        /// <summary>
        /// An <see cref="ICommand"/> to navigate back in navigation stack.
        /// </summary>
        public ICommand GoBackCommand =>
            new RelayCommand(() =>
            {
                if (CanGoBack())
                {
                    GoBack();
                }
            });

        /// <summary>
        /// Initializes a new instance of <see cref="ViewModelWithNavigationCommands"/>.
        /// </summary>
        /// <param name="source">The source view model.</param>
        public ViewModelWithNavigationCommands(IRedirectViewModel source)
        {
            _source = source;
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
        /// Determines if view model can navigate forward.
        /// </summary>
        /// <param name="steps">The number of steps to make.</param>
        /// <returns>True if view model can navigate forward.</returns>
        public bool CanGoForward(int steps = 1)
        {
            return _source.CanGoForward(steps);
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
        /// Navigates the current child back.
        /// </summary>
        /// <param name="steps">The number of steps to make.</param>
        public void GoBack(int steps = 1)
        {
            _source.GoBack(steps);
        }

        /// <summary>
        /// Navigates the current child to a new view model.
        /// </summary>
        /// <param name="viewModel"></param>
        public void NavigateTo(object viewModel)
        {
            _source.NavigateTo(viewModel);
        }
    }
}
