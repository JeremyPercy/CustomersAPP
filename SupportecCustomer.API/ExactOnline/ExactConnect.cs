using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace SupportecCustomer.API.ExactOnline
{
    public class ExactConnect
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _client;

        public ExactConnect(IConfiguration configuration, HttpClient client)
        {
            client.BaseAddress = new Uri("https://start.exactonline.nl");
            _client = client;
            _config = configuration;
        }

        public Task getMe()
        {
            // var url = new Uri("/api/oauth2/auth?client_id=" + _config.GetSection("ExactConnectionStrings:ClientId") + "&redirect_uri=" + _config.GetSection("ExactConnectionStrings:RedirectUrl") + "&response_type=" + _config.GetSection("ExactConnectionStrings:ResponseType") + "&force_login=" + _config.GetSection("ExactConnectionStrings:ForceLogin");
            return _client.GetAsync(
                     "/api/v1/current/me");
        }
    }
}