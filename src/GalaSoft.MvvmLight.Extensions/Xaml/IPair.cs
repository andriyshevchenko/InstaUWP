using System;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    public interface IPair
    {
        Type View { get; set; }
        Type ViewModel { get; set; }
    }
}