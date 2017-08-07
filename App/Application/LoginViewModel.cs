using GalaSoft.MvvmLight.Extensions.Core;
using GalaSoft.MvvmLight.Extensions;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Net.Http;
using Nito.Mvvm;
using InstaSharper;
using InstaSharper.API;
using InstaSharper.API.Builder;
using InstaSharper.Classes;

namespace App.ViewModel
{
    /// <summary>
    /// The entry view model for an application
    /// </summary>
    public class LoginViewModel : RedirectViewModel
    {
        public ICommand LoginCommand => new AsyncCommand(Proceed);

        private static readonly HttpClient _http = new NeverCloseHttp();

        private InstaApi _api = new WrapApi(new SessionData(), _http);

        /// <summary>
        /// "Main" equivalent.
        /// </summary>
        /// <returns>New <see cref="Task"/> object.</returns>
        private async Task Proceed()
        {
            NavigateTo(new PleaseWaitViewModel(this));

            await AsynchronousOperation();

            var profileViewModel = new ProfileViewModel("", this);

            NavigateTo(profileViewModel);

            //no code is running below except an asynchronous operation, so use ConfigureAwait(false)
            await profileViewModel.FetchData().ConfigureAwait(false);
        }

        public async Task AsynchronousOperation()
        {
            await Task.Delay(System.TimeSpan.FromSeconds(3)).ConfigureAwait(false);
            //return await _api.LoginAsync().ConfigureAwait(false);
        }

        public LoginViewModel(INavigationRoot root, string childName) : base(root, childName)
        {

        }
    }
}
