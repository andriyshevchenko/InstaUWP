using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Extensions.Core;
using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Extensions;

namespace App.ViewModel
{
    public class BlankViewModel : RedirectViewModel
    {
        public ICommand MakeErrorCommand => new RelayCommand(MakeError);

        private void MakeError()
        {
            throw new NotImplementedException();
        }

        public BlankViewModel(INavigationRoot root, string childName = MainChild) : base(root, childName)
        {
        }
    }
}
