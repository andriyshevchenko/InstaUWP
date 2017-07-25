using System;

namespace App
{
    public class Locator 
    {
        static Lazy<MainViewModel> _mainViewModel = new Lazy<MainViewModel>(true);
        public static MainViewModel MainViewModel => _mainViewModel.Value;
    }
}
