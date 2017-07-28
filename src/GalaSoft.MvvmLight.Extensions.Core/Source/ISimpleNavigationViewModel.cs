using System.Windows.Input;

namespace GalaSoft.MvvmLight.Extensions
{
    /// <summary>
    /// View model which support a simple navigation.
    /// </summary>
    public interface ISimpleNavigationViewModel
    {
        /// <summary>
        /// An <see cref="ICommand"/> to navigate back.
        /// </summary>
        ICommand GoBackCommand { get; }

        /// <summary>
        /// An <see cref="ICommand"/> to navigate forward.
        /// </summary>
        ICommand GoForwardCommand { get; }
    }
}
