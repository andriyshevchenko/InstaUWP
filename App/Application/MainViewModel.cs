using GalaSoft.MvvmLight.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Application
{
    public class MainViewModel : HostViewModel
    {
        public MainViewModel()
        {
            NavigateTo("main", new BlankViewModel());
        }
    }
}
