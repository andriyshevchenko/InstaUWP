using GalaSoft.MvvmLight.Extensions.Core;
using GalaSoft.MvvmLight.Extensions;
using System.Windows.Input;

namespace ExampleApplication
{
    public class BlankPageViewModel : RedirectViewModel
    {
        public ICommand GoToOtherCommand => new NavigateToCommand(this, new OtherPageViewModel(this));

        public BlankPageViewModel(RedirectViewModel other) : base(other)
        { 

        }

        public BlankPageViewModel(INavigationRoot root, string childName = "main") : base(root, childName)
        {

        }
    }
}
