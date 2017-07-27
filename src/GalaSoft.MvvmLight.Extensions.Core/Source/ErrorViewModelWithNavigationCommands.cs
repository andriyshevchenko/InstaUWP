using System.Windows.Input;

namespace GalaSoft.MvvmLight.Extensions
{
    public class ErrorViewModelWithNavigationCommands : IErrorViewModel, ISimpleNavigationViewModel
    {
        private ISimpleNavigationViewModel _source;
        private IErrorViewModel _error;

        public ErrorViewModelWithNavigationCommands(IErrorViewModel source)
        {
            _error = source;
            _source = new ViewModelWithNavigationCommands(source);
        }

        public ICommand GoForwardCommand => _source.GoForwardCommand;

        public ICommand GoBackCommand => _source.GoBackCommand;

        public string Error => _error.Error;
  
        public bool CanGoBack(int steps = 1)
        {
            return _error.CanGoBack(steps);
        }

        public bool CanGoForward(int steps = 1)
        {
            return _error.CanGoForward(steps);
        }

        public void GoBack(int steps = 1)
        {
            _error.GoBack(steps);
        }

        public void GoForward(int steps = 1)
        {
            _error.GoForward(steps);
        }

        public void NavigateTo(object viewModel)
        {
            _error.NavigateTo(viewModel);
        }
    }
}
