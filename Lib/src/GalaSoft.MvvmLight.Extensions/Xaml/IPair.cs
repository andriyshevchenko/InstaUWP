using System;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    /// <summary>
    /// View-ViewModel pair
    /// </summary>
    public interface IPair
    {
        /// <summary>
        /// The view type.
        /// </summary>
        Type ViewType { get; }
        
        /// <summary>
        /// The view model type.
        /// </summary>
        Type ViewModelType { get; }
    }
}