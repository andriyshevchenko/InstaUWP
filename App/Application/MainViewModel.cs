using GalaSoft.MvvmLight.Extensions;

namespace App
{
    public class MainViewModel : HostViewModel
    {
        public MainViewModel()
        {
            NavigateTo("main", new BlankViewModel());
        }
    }
}
