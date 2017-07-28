namespace GalaSoft.MvvmLight.Extensions
{
    /// <summary>
    /// Defines a view model to signal about error, which is also a <see cref="IRedirectViewModel"/>.
    /// </summary>
    public interface IErrorViewModel : IRedirectViewModel
    {
        /// <summary>
        /// The error message.
        /// </summary>
        string Error { get; }
    }
}
