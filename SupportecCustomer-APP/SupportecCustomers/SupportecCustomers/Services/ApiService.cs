using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SupportecCustomers.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SupportecCustomers.Services
{
    public class ApiService
    {
        public static string BaseAddress = "https://supportecc.azurewebsites.net/api/";
        readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<bool> RegisterUser(string email, string password)
        {
            var registerModel = new LoginRegister()
            {
                Email = email,
                Password = password
            };

            var json = JsonConvert.SerializeObject(registerModel);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(BaseAddress + "auth/register", content);

            return response.IsSuccessStatusCode;
        }

        public async Task<AccesToken> GetToken(string email, string password)
        {
            var user = new LoginRegister()
            {
                Email = email,
                Password = password
            };

            var json = JsonConvert.SerializeObject(user);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(BaseAddress + "auth/login", content);

            var jsonResult = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<AccesToken>(jsonResult);

            return result;
        }

        public async Task<List<Product>> GetAllProductsOfUser(int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("token", ""));
            var response = await _httpClient.GetStringAsync(BaseAddress + "products/user/" + id);
            return JsonConvert.DeserializeObject<List<Product>>(response);
        }

        public async Task<Product> GetProduct(int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("token", ""));
            var response = await _httpClient.GetStringAsync(BaseAddress + "products/" + id);
            return JsonConvert.DeserializeObject<Product>(response);
        }

        public async Task<decimal> GetProductPrice(int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("token", ""));
            var response = await _httpClient.GetStringAsync(BaseAddress + "products/totalprice/" + id);
            return JsonConvert.DeserializeObject<decimal>(response);
        }

        public int GetUserId()
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtoken = handler.ReadJwtToken(Preferences.Get("token", ""));

            return int.Parse(jwtoken.Claims.First(claim => claim.Type == "nameid").Value);
        }

        public async Task<User> GetUser()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("token", ""));
            int id = GetUserId();
            var response = await _httpClient.GetStringAsync(BaseAddress + "users/" + id);
            return JsonConvert.DeserializeObject<User>(response);
        }

        public async Task<bool> UpdateUser(User user)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("token", ""));
            int id = GetUserId();

            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(BaseAddress + "users/" + id, content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateCompany(Company company)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("token", ""));
            int id = GetUserId();

            var json = JsonConvert.SerializeObject(company);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(BaseAddress + "users/company/update/" + id, content);

            return response.IsSuccessStatusCode;
        }
    }
}
