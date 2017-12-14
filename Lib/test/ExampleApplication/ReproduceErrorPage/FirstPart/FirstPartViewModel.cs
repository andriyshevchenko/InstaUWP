using GalaSoft.MvvmLight.Extensions;
using GalaSoft.MvvmLight.Extensions.Core;
using System.Windows.Input;

namespace ExampleApplication
{
    public class FirstPartViewModel : RedirectViewModel
    {
        public ICommand BlankCommand => new NavigateToCommand(this, new BlankPageViewModel(this));

        public FirstPartViewModel(INavigationRoot root, string childName = "main") : base(root, childName)
        {
        }
    }
}
