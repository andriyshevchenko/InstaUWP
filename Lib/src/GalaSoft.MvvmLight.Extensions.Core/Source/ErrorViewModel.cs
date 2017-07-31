using GalaSoft.MvvmLight.Extensions.Core;
using System;

namespace GalaSoft.MvvmLight.Extensions
{
    /// <summary>
    /// Simple error view model which is able to redirect: <see cref="RedirectViewModel"/>
    /// </summary>
    public class ErrorViewModel : RedirectViewModel, IErrorViewModel
    {
        /// <summary>
        /// The error message.
        /// </summary>
        public string Error { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="ErrorViewModel"/>.
        /// </summary>
        /// <param name="root">The parent.</param>
        /// <param name="error">An error.</param>
        /// <param name="childName">The name of the child.</param>
        public ErrorViewModel(INavigationRoot root, string error, string childName = MainChild) : this(root, childName)
        {
            Error = error;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ErrorViewModel"/>.
        /// </summary>
        /// <param name="root">The parent.</param>
        /// <param name="exception">An error.</param>
        /// <param name="childName">The name of the child.</param>
        public ErrorViewModel(INavigationRoot root, Exception exception, string childName = MainChild) : this(root, exception.ToString(), childName)
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="ErrorViewModel"/>.
        /// </summary>
        /// <param name="root">The parent.</param>
        /// <param name="childName">The name of the child.</param>
        public ErrorViewModel(INavigationRoot root, string childName = MainChild) : base(root, childName)
        {
        }
    }
}
