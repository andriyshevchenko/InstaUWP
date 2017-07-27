using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;

namespace App
{
    public class BlankViewModel
    {
        public ICommand MakeErrorCommand => new RelayCommand(MakeError);

        private void MakeError()
        {
            throw new NotImplementedException();
        }

        public BlankViewModel()
        {
            
        }
    }
}
