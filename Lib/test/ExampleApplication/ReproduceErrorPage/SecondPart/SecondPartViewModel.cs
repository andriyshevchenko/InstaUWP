using GalaSoft.MvvmLight.Extensions.Core;
using GalaSoft.MvvmLight.Extensions;
using System.Windows.Input;

namespace ExampleApplication 
{
    public class SecondPartViewModel : RedirectViewModel
    {
        public ICommand OpenMoreCommand => new NavigateToCommand(this, new SeeMoreViewModel(this));

        public SecondPartViewModel(INavigationRoot root, string childName = "main") : base(root, childName)
        {
        }
    }
}
