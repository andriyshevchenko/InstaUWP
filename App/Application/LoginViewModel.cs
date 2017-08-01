using GalaSoft.MvvmLight.Extensions.Core;
using GalaSoft.MvvmLight.Extensions;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace App.ViewModel
{
    public class LoginViewModel : RedirectViewModel
    {
        public ICommand LoginCommand => new RelayCommand(Proceed);

        private void Proceed()
        {
            NavigateTo(new PleaseWaitViewModel(this));
        }

        public LoginViewModel(INavigationRoot root, string childName) : base(root, childName)
        {
        }
    }
}
