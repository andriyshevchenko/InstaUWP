using GalaSoft.MvvmLight.Extensions.Core;
using GalaSoft.MvvmLight.Extensions;

namespace App.ViewModel
{
    public class BlankViewModel : RedirectViewModel
    {
        public BlankViewModel(INavigationChild source) : this(source.Root, source.ChildName)
        {

        }

        public BlankViewModel(INavigationRoot root, string childName = MainChild) : base(root, childName)
        {
        }
    }
}
