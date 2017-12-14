using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Extensions;
using System.Windows.Input;
using GalaSoft.MvvmLight.Extensions.Core;

namespace ExampleApplication
{
    public class OtherPageViewModel : RedirectViewModel
    {
        public ICommand ReproduceErrorCommand => new NavigateToCommand(this, new ReproduceErrorViewModel(this));

        public OtherPageViewModel(INavigationChild other) : base(other)
        {

        }

        public OtherPageViewModel(INavigationRoot root, string childName) : base(root, childName)
        {

        }
    }
}
