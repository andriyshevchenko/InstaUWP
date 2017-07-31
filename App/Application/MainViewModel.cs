using GalaSoft.MvvmLight.Extensions;

namespace App.ViewModel
{
    public class MainViewModel : HostViewModel
    {
        public MainViewModel()
        {
            NavigateTo(new BlankViewModel(this, "main"));
        }
    }
}
