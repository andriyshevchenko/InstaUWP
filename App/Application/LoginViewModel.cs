using GalaSoft.MvvmLight.Extensions.Core;
using GalaSoft.MvvmLight.Extensions;
using System.Windows.Input;
using Nito.Mvvm;
using System.Threading.Tasks;
using InstaSharp;
using System.Net.Http;
using App.Domain;

using static System.Collections.Generic.Create;
 
namespace App.ViewModel
{
    /// <summary>
    /// The entry view model for an application
    /// </summary>
    public class LoginViewModel : RedirectViewModel
    {
        public ICommand LoginCommand => new AsyncCommand(Proceed);

        private static readonly HttpClient _http = new HttpClient();

        private Properties.InstagramConfig _config = new Properties.InstagramConfig();

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
            //artificial delay
            await Task.Delay(30).ConfigureAwait(false);

            var config = new WrapConfig(_config);

            var scopes = list(OAuth.Scope.Likes, OAuth.Scope.Comments);

            var link =
                OAuth.AuthLink(
                    config.OAuthUri + "authorize",
                    config.ClientId,
                    config.RedirectUri,
                    scopes,
                    OAuth.ResponseType.Code
                );

            var message = await _http.GetAsync(link).ConfigureAwait(false);
            return await message.Content.ReadAsStringAsync().ConfigureAwait(false);
         }

        public LoginViewModel(INavigationRoot root, string childName) : base(root, childName)
        {

        }
    }
}
