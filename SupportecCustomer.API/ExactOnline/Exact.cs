using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace SupportecCustomer.API.ExactOnline
{

    [Route("api/exact")]
    [ApiController]
    public class Exact : Controller
    {
        private readonly IHttpClientFactory _client;

        public Exact(IHttpClientFactory client)
        {
            _client = client;
        }

        [HttpGet("token")]
        public async Task<IActionResult> getToken()
        {
            return Challenge(new AuthenticationProperties() { RedirectUri = "/api/exact/me" });
        }

        [AllowAnonymous]
        [HttpGet("me")]
        public async Task<IActionResult> MeAsync()
        {
            string accessToken = await HttpContext.GetTokenAsync("access_token");
            var auth = await HttpContext.AuthenticateAsync("Cookies");
            return Ok(auth);
        }

        // public async Task<IActionResult> CallApiUsingUserRefreshToken()
        // {
        //     var oldAccessToken = await HttpContext.GetTokenAsync("access_token");
        //     var oldRefreshToken = await HttpContext.GetTokenAsync("refresh_token");

        //     var tokenClient = new AuthenticationToken
        //     var newToken = await tokenClient.RequestRefreshTokenAsync(oldRefreshToken);

        //     var client = new HttpClient();
        //     client.SetBearerToken(newToken.AccessToken);
        //     var content = await client.GetStringAsync("http://localhost:5001/identity");

        //     ViewBag.Json = JArray.Parse(content).ToString();
        //     return View("json");
        // }


    }
}