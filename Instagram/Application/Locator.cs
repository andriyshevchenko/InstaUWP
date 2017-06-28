using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Application
{
    public class Locator 
    {
        static Lazy<MainViewModel> _mainViewModel = new Lazy<MainViewModel>(true);
        public static MainViewModel MainViewModel => _mainViewModel.Value;
    }
}
