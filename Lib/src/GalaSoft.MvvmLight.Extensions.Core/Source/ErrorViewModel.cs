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
        /// <param name="childName">The name of the child.</param>
        /// <param name="error"></param>
        public ErrorViewModel(INavigationRoot root, string childName, string error) :this(root, childName)
        {
            Error = error;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ErrorViewModel"/>.
        /// </summary>
        /// <param name="root">The parent.</param>
        /// <param name="childName">The name of the child.</param>
        /// <param name="exception">An error.</param>
        public ErrorViewModel(INavigationRoot root, string childName, Exception exception) : this(root, childName, exception.ToString())
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="ErrorViewModel"/>.
        /// </summary>
        /// <param name="root">The parent.</param>
        /// <param name="childName">The name of the child.</param>
        public ErrorViewModel(INavigationRoot root, string childName) : base(root, childName)
        {
        }
    }
}
