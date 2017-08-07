using GalaSoft.MvvmLight.Extensions.Core;
using GalaSoft.MvvmLight.Extensions;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Net.Http;
using Nito.Mvvm;
using InstaSharper.API;
using InstaSharper.Classes;

namespace App.ViewModel
{
    /// <summary>
    /// The entry view model for an application
    /// </summary>
    public class LoginViewModel : RedirectViewModelWithContent
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

            var loginResult = await AsynchronousOperation();

            object viewModel;
            if (!loginResult.Succeeded)
            {
                viewModel =  new BadCredentialViewModel(Reason.InvalidLogin));
            }
            else
            {
                viewModel = new BlankViewModel();
            }
            NavigateTo("dock", viewModel);

            var profileViewModel = new ProfileViewModel("", this);

            NavigateTo(profileViewModel);

            //no code is running below except an asynchronous operation, so use ConfigureAwait(false)
            await profileViewModel.FetchData().ConfigureAwait(false);
        }

        public async Task<IResult<bool>> AsynchronousOperation()
        {
           return await _api.LoginAsync().ConfigureAwait(false);
        }

        public LoginViewModel(INavigationRoot root, string childName) : base(root, childName)
        {
        }
    }
}
