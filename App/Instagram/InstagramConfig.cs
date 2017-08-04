using Cactoos;
using System.Collections.Generic;

using static System.Collections.Generic.Create;

namespace App.Properties
{
    public class InstagramConfig : IScalar<Dictionary<string, string>>
    {
        public Dictionary<string, string> Value()
        {
            return
                dictionary(
                    ("client_id", "0722d92cc1cd40f087b2536b0d390128"),
                    ("client_pass", "7ec2280b3f254614aceed752bd34dff2"),
                    ("email", "shewchenkoandriy@gmail.com"),
                    ("client_status", "Sandbox Mode")
                );
        }
    }
}
