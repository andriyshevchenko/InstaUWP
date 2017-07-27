using GalaSoft.MvvmLight.Extensions.Core;
using System;

namespace GalaSoft.MvvmLight.Extensions
{
    public class ErrorViewModel : RedirectViewModel, IErrorViewModel
    {
        public string Error { get; }

        public ErrorViewModel(INavigationRoot root, string childName, string error) :this(root, childName)
        {
            Error = error;
        }

        public ErrorViewModel(INavigationRoot root, string childName, Exception exception) : this(root, childName, exception.ToString())
        {

        }

        public ErrorViewModel(INavigationRoot root, string childName) : base(root, childName)
        {
        }
    }
}
