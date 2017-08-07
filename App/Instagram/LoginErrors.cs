using Cactoos;
using System.Collections.Generic;

using static System.Collections.Generic.Create;

namespace App
{
    public class LoginErrors : IScalar<Dictionary<string, string>>
    {
        public Dictionary<string, string> Value()
        {
            return dictionary(
                       ("empty_login", "The username cannot be empty"),
                       ("empty_password", "The password cannot be empty"),
                       ("invalid_login", "The username you entered doesn't belong to an account. Please check your username and try again."),
                       ("invalid_password", "Sorry, your password was incorrect. Please double-check your password.")
                   );
        }
    }
}
