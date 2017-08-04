using GalaSoft.MvvmLight.Extensions.Core;
using GalaSoft.MvvmLight.Extensions;
using System.Windows.Input;
using Nito.Mvvm;
using System.Threading.Tasks;
using System;

using static System.Functional.FlowControl;

namespace App.ViewModel
{
    public class LoginViewModel : RedirectViewModel
    {
        public ICommand LoginCommand => new AsyncCommand(Proceed);

        /// <summary>
        /// 
        /// </summary>
        /// <returns>New <see cref="Task"/> object.</returns>
        private async Task Proceed()
        {
            NavigateTo(new PleaseWaitViewModel(this));
            await AsynchronousOperation();

            var profileViewModel = new ProfileViewModel(this);

            NavigateTo(profileViewModel);

            using (profileViewModel.Initialize())
            {
                await profileViewModel.FetchData();
            }
        }

        private async Task AsynchronousOperation()
        {
            await Task.Delay(TimeSpan.FromSeconds(3)).ConfigureAwait(false);
        }

        public LoginViewModel(INavigationRoot root, string childName) : base(root, childName)
        {

        }
    }
}
