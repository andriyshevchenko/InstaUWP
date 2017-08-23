using Cactoos;
using InstaSharper.API;
using InstaSharper.Classes;
using System.Net.Http;
using System;
using InstaSharper.API.Builder;

namespace App
{
    public class WrapApi : IScalar<IInstaApi>
    {
        private UserSessionData _user;
        private HttpClient _client;

        public WrapApi(UserSessionData user, HttpClient httpClient) 
        {
            _user = user;
            _client = httpClient;
        }

        public IInstaApi Value()
        {
            return new InstaApiBuilder()
                .SetUser(_user)
                .UseHttpClient(_client)
                .Build();
        }
    }
}
