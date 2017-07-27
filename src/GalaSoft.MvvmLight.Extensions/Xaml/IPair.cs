using System;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    public interface IPair
    {
        Type View { get; }
        Type ViewModel { get; }
    }
}