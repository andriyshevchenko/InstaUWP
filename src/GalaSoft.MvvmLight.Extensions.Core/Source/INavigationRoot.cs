namespace GalaSoft.MvvmLight.Extensions
{
    public interface INavigationRoot
    {
        bool CanGoBack(string childName, int steps = 1);
        bool CanGoForward(string childName, int steps = 1);

        void GoForward(string childName, int steps = 1);
        void GoBack(string childName, int steps = 1);
        void NavigateTo(string childName, object viewModel);
    }
}
