using GalaSoft.MvvmLight.Extensions;

namespace TestApplication
{
    public class MainViewModel : HostViewModel
    {
        public MainViewModel()
        {
            NavigateTo("main", new OtherPageViewModel(this, "main"));
        }
    }
}
