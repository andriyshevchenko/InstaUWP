using GalaSoft.MvvmLight.Extensions.Core;
using System;
using System.Threading.Tasks;

namespace App.ViewModel
{
    public class ProfileViewModel : RedirectViewModel
    {
        public string Message { get; }

        public async Task FetchData()
        {
            await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
        }
 
        public ProfileViewModel(string source, RedirectViewModel other) : base(other)
        {
            Message = source;
        }
    }
}
