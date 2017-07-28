using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace GalaSoft.MvvmLight.Extensions
{

    public class ViewModelWithNavigationCommands : IRedirectViewModel, ISimpleNavigationViewModel
    {
        private IRedirectViewModel _source;

        public ICommand GoForwardCommand =>
            new RelayCommand(() =>
            {
                if (CanGoForward())
                {
                    GoForward();
                }
            });

        public ICommand GoBackCommand =>
            new RelayCommand(() =>
            {
                if (CanGoBack())
                {
                    GoBack();
                }
            });

        public ViewModelWithNavigationCommands(IRedirectViewModel source)
        {
            _source = source;
        }

        public bool CanGoBack(int steps = 1)
        {
            return _source.CanGoBack(steps);
        }

        public bool CanGoForward(int steps = 1)
        {
            return _source.CanGoForward(steps);
        }

        public void GoForward(int steps = 1)
        {
            _source.GoForward(steps);
        }

        public void GoBack(int steps = 1)
        {
            _source.GoBack(steps);
        }

        public void NavigateTo(object viewModel)
        {
            _source.NavigateTo(viewModel);
        }
    }
}
