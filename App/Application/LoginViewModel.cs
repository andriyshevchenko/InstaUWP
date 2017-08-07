﻿using GalaSoft.MvvmLight.Extensions.Core;
using GalaSoft.MvvmLight.Extensions;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Net.Http;
using Nito.Mvvm;
using InstaSharper.API;
using InstaSharper.Classes;
using Cactoos.Text;
using Cactoos;
using Cactoos.Scalar;

namespace App.ViewModel
{
    /// <summary>
    /// The entry view model for an application
    /// </summary>
    public class LoginViewModel : RedirectViewModelWithContent
    {
        public ICommand LoginCommand => new AsyncCommand(Proceed);

        private static readonly HttpClient _http = new NeverCloseHttp();

        private IScalar<InstaApi> _api;

        /// <summary>
        /// "Main" equivalent.
        /// </summary>
        /// <returns>New <see cref="Task"/> object.</returns>
        private async Task Proceed()
        {
            //artificial delay
            await AsynchronousDelay();

            NavigateTo(new PleaseWaitViewModel(this));

            var loginResult = await AsynchronousOperation();

            if (loginResult.Succeeded)
            {
                var profileViewModel = new ProfileViewModel("The message", this);

                NavigateTo(profileViewModel);

                //no code is running below except an asynchronous operation, so use ConfigureAwait(false)
                await profileViewModel.FetchData().ConfigureAwait(false);
            }
            else
            {
                NavigateTo("dock", new BadCredentialViewModel(Reason.InvalidLogin));
            }
        }

        private async Task AsynchronousDelay()
        {
            await Task.Delay(30).ConfigureAwait(false);
        }

        public async Task<IResult<bool>> AsynchronousOperation()
        {
            return await _api.Value().LoginAsync().ConfigureAwait(false);
        }

        public LoginViewModel(INavigationRoot root, string childName) : base(root, childName)
        {
            _api = new LazyScalar<InstaApi>(() =>
                        new WrapApi(
                            new SessionData(Login, Password), _http
                        )
                   );
        }

        private void ValidatePassword()
        {
            if (new IsBlank(Password).Value())
            {
                NavigateTo("dock", new BadCredentialViewModel(Reason.EmptyPassword));
            }
        }

        private void ValidateLogin()
        {
            if (new IsBlank(Login).Value())
            {
                NavigateTo("dock", new BadCredentialViewModel(Reason.EmptyLogin));
            }
        }


        private string _login;

        public string Login
        {
            get { return _login; }
            set
            {
                ValidateLogin();
                Set(ref _login, value);
            }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                ValidatePassword();
                Set(ref _password, value);
            }
        }
    }
}
