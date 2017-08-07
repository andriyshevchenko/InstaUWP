using Cactoos;
using System.Collections.Generic;

namespace App
{
    public enum Reason
    {
        InvalidLogin,
        InvalidPassword
    }

    public class BadCredentialViewModel
    {
        public string Message { get; }

        public BadCredentialViewModel()
        {
            Message = string.Empty;
        }

        public BadCredentialViewModel(Reason reason) : this(new LoginErrors(), reason)
        {

        }

        public BadCredentialViewModel(IScalar<Dictionary<string, string>> source, Reason reason)
        {
            var errors = source.Value();
            if (reason == Reason.InvalidLogin)
            {
                Message = errors["invalid_login"];
            }
            else
            {
                Message = errors["invalid_password"];
            }
        }
    }
}
