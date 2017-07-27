using InputValidation;

namespace GalaSoft.MvvmLight.Extensions.Core
{
    public class RedirectViewModel : IRedirectRoot
    {
        private INavigationRoot _root;
        private string _child;

        public RedirectViewModel(INavigationRoot root, string childName)
        {
            _root = root.CheckNotNull("navigation root");
            _child = childName.CheckNotNull(nameof(childName));
        }

        public bool CanGoBack(int steps = 1)
        {
            return _root.CanGoBack(_child, steps);
        }

        public bool CanGoForward(int steps = 1)
        {
            return _root.CanGoForward(_child, steps);
        }

        public void GoBack(int steps = 1)
        {
            _root.GoBack(_child, steps);
        }

        public void GoForward(int steps = 1)
        {
            _root.GoForward(_child, steps);
        }

        public void NavigateTo(object viewModel)
        {
            _root.NavigateTo(_child, viewModel);
        }
    }
}
