using GalaSoft.MvvmLight.Extensions;

namespace App.ViewModel
{
    public class MainViewModel : HostViewModel
    {
        public MainViewModel()
        {
            NavigateTo("main", new LoginViewModel(this, "main"));
        }
    }
}
