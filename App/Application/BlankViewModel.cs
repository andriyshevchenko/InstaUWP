using GalaSoft.MvvmLight.Extensions.Core;
using GalaSoft.MvvmLight.Extensions;

namespace App.ViewModel
{
    public class BlankViewModel : RedirectViewModel
    {
        public BlankViewModel(INavigationRoot root, string childName = MainChild) : base(root, childName)
        {
        }
    }
}
