using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Extensions;
using System.Windows.Input;
using System;

namespace TestApplication
{
    public class OtherPageViewModel : GalaSoft.MvvmLight.Extensions.Core.RedirectViewModel
    {
        public ICommand ReproduceErrorCommand => new RelayCommand(ReproduceError);

        private void ReproduceError()
        {
            NavigateTo(new ReproduceErrorViewModel(this));
        }

        public OtherPageViewModel(INavigationRoot root, string childName) : base(root, childName)
        {

        }
    }
}
