using GalaSoft.MvvmLight.Extensions;

namespace Instagram.Application
{
    public class MainViewModel : HostViewModel
    {
        public MainViewModel()
        {
            NavigateTo("main", new BlankViewModel());
        }
    }
}
