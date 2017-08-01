using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Extensions.Core;
using System.Windows.Input;

namespace App.ViewModel 
{
    public class PleaseWaitViewModel : RedirectViewModel
    {
        public ICommand CancelCommand => new RelayCommand(Abort);

        public void Abort()
        {
            if (CanGoBack())
            {
                GoBack();
            }
        }

        public PleaseWaitViewModel(RedirectViewModel other) : base(other)
        {

        }
    }
}
