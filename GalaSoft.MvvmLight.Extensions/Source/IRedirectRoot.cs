namespace GalaSoft.MvvmLight.Extensions
{
    public interface IRedirectRoot
    {
        bool CanGoBack(int steps = 1);
        bool CanGoForward(int steps = 1);

        void GoForward(int steps = 1);
        void GoBack(int steps = 1);
        void NavigateTo(object viewModel);
    }
}
