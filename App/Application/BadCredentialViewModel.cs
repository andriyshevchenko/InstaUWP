using Cactoos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.ViewModel
{
    public enum Reason
    {
        InvalidLogin,
        InvalidPassword,
        EmptyLogin,
        EmptyPassword
    }

    public class BadCredentialViewModel
    {
        public string Message { get; }

        public BadCredentialViewModel(IAttempt attempt) : this(attempt.Errors())
        {

        }

        public BadCredentialViewModel(Exception[] e) : this(e.First().Message)
        {

        }

        public BadCredentialViewModel(string message) : this(new Cactoos.Text.Text(message))
        {

        }

        public BadCredentialViewModel(IText message)
        {
            Message = message.String();
        }

        public BadCredentialViewModel(Reason reason) : this(new LoginErrors(), reason)
        {

        }

        public BadCredentialViewModel(IScalar<Dictionary<string, string>> source, Reason reason)
        {
            var errors = source.Value();
            switch (reason)
            {
                case Reason.InvalidLogin:
                    Message = errors["invalid_login"];
                    break;
                case Reason.InvalidPassword:
                    Message = errors["invalid_password"];
                    break;
                case Reason.EmptyLogin:
                    Message = errors["empty_login"];
                    break;
                case Reason.EmptyPassword:
                    Message = errors["empty_password"];
                    break;
                default:
                    break;
            }
        }
    }
}
