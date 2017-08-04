using GalaSoft.MvvmLight.Extensions.Core;
using Nito.Disposables;
using System;
using System.Threading.Tasks;

namespace App.ViewModel
{
    public class ProfileViewModel : RedirectViewModel
    {
        public async Task FetchData()
        {
            await Task.Delay(TimeSpan.FromSeconds(3)).ConfigureAwait(false);
        }

        public AnonymousDisposable Initialize()
        {
            return new AnonymousDisposable(() => 
            {
                if (CanGoBack(2))
                {
                    GoBack(2);
                }
            });
        }

        public ProfileViewModel(RedirectViewModel other) : base(other)
        {

        }
    }
}
