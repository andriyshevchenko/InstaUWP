using InstaSharper.API;
using InstaSharper.Classes;
using System.Net.Http;

namespace App
{
    public class WrapApi : InstaApi
    {
        public WrapApi(UserSessionData user, HttpClient httpClient) : base(user, null, httpClient, null, null, null)
        {
        }
    }
}
