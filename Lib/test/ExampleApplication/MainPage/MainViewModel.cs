using GalaSoft.MvvmLight.Extensions;

namespace ExampleApplication
{
    public class MainViewModel : HostViewModel
    {
        public MainViewModel()
        {
            NavigateTo("main", new BlankPageViewModel(this));
        }
    }
}
