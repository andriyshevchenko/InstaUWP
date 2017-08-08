using GalaSoft.MvvmLight.Extensions.Core;
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
using Cactoos.Scalar.Async;
using Cactoos.List;

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
            if (!Validate(Login, Reason.EmptyLogin) || !Validate(Password, Reason.EmptyPassword))
            {
                return;
            }

            //artificial delay
            await AsynchronousDelay();

            NavigateTo(new PleaseWaitViewModel(this));

            var login =
                new ErrorSafeAsyncScalar<IResult<bool>>(
                    () => _api.Value().LoginAsync(),
                    () => Result.Fail<bool>("an exception was thrown")
                );

            var loginResult = await login.Value();

            if (loginResult.Succeeded)
            {
                var profileViewModel = new ProfileViewModel("The message", this);

                NavigateTo(profileViewModel);

                //no code is running below except an asynchronous operation, so use ConfigureAwait(false)
                await profileViewModel.FetchData().ConfigureAwait(false);
            }
            else
            {
                GoBack();
                NavigateTo("dock", new BadCredentialViewModel(login.Errors()));
                Set(nameof(Login), ref _login, string.Empty);
                Set(nameof(Password), ref _password, string.Empty);
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

        private bool Validate(string value, Reason reason)
        {
            bool blank = new IsBlank(value).Value();
            if (blank)
            {
                NavigateTo("dock", new BadCredentialViewModel(reason));
            }
            return !blank;
        }

        private string _login;

        public string Login
        {
            get { return _login; }
            set
            {
                Validate(value, Reason.EmptyLogin);
                Set(ref _login, value);
            }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                Validate(value, Reason.EmptyPassword);
                Set(ref _password, value);
            }
        }
    }
}
