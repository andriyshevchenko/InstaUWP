using System.Windows.Input;

namespace GalaSoft.MvvmLight.Extensions
{
    public interface ISimpleNavigationViewModel
    {
        ICommand GoBackCommand { get; }
        ICommand GoForwardCommand { get; }
    }
}
