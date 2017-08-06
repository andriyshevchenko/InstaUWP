using GalaSoft.MvvmLight.Extensions.Core;
using GalaSoft.MvvmLight.Extensions;
using System.Windows.Input; 
using System.Threading.Tasks;
using System.Net.Http;
using Nito.Mvvm;

namespace App.ViewModel
{
    /// <summary>
    /// The entry view model for an application
    /// </summary>
    public class LoginViewModel : RedirectViewModel
    {
        public ICommand LoginCommand => new AsyncCommand(Proceed);

        private static readonly HttpClient _http = new NeverCloseHttp();

        /// <summary>
        /// "Main" equivalent.
        /// </summary>
        /// <returns>New <see cref="Task"/> object.</returns>
        private async Task Proceed()
        {
            NavigateTo(new PleaseWaitViewModel(this));

            var profileViewModel = new ProfileViewModel(await AsynchronousOperation(), this);

            NavigateTo(profileViewModel);

            //no code is running below except asynchronous operation, so use ConfigureAwait(false)
            await profileViewModel.FetchData().ConfigureAwait(false);
        }

        public async Task<string> AsynchronousOperation()
        {
            return "";
        }

        public LoginViewModel(INavigationRoot root, string childName) : base(root, childName)
        {

        }
    }
}
