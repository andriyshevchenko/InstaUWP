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
                       ("invalid_login", "The username you entered doesn't belong to an account. Please check your username and try again."),
                       ("invalid_password", "Sorry, your password was incorrect. Please double-check your password.")
                   );
        }
    }
}
