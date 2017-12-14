using GalaSoft.MvvmLight.Extensions.Core;

namespace ExampleApplication
{
    public class SeeMoreViewModel : RedirectViewModel
    {
        public SeeMoreViewModel(RedirectViewModel other) : base(other)
        {
        }
    }
}
