using GalaSoft.MvvmLight.Extensions.Core;
using GalaSoft.MvvmLight.Extensions;

namespace App.ViewModel
{
    public class LoginViewModel : RedirectViewModel
    {
        public LoginViewModel(INavigationRoot root, string childName) : base(root, childName)
        {
        }
    }
}
