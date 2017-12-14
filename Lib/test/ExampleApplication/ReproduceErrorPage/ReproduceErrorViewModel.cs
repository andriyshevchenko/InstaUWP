using GalaSoft.MvvmLight.Extensions;
using GalaSoft.MvvmLight.Extensions.Core;

namespace ExampleApplication
{
    public class ReproduceErrorViewModel : RedirectViewModelWithContent
    {
        public ReproduceErrorViewModel(INavigationChild source) : base(source)
        {
            NavigateTo("dock", new SecondPartViewModel(this, "dock"));
            NavigateTo("content", new FirstPartViewModel(this, "content"));
        }
    }
}
