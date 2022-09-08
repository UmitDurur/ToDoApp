using IdentityModel.Client;
using IdentityServer4.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Serilog;
using System.Net.Http.Headers;
using Volo.Abp.DependencyInjection;

namespace ToDoFrontEnd.Services
{
    public class HttpService : ISingletonDependency
    {
        private static User _user;
        public HttpService()
        {

        }
        public HttpService(string username,string password)
        {
            _user =new User( username,password );
        }
        public async Task<Lazy<HttpClient>> GetHttpClientAsync(bool setBearerToken)
        {
            var client = new Lazy<HttpClient>(() => new HttpClient());
            var accessToken = await GetAccessToken();
            
            if (setBearerToken)
            {
                client.Value.SetBearerToken(accessToken);
            }
            client.Value.BaseAddress = new Uri("https://localhost:44352/"); //
            return await Task.FromResult(client);
        }

        private static async Task<TokenResponse> GetTokensFromToDoApi()
        {
            var authority = "https://localhost:44352/";
            var discoveryCache = new DiscoveryCache(authority);
            var disco = await discoveryCache.GetAsync();
            
            var httpClient = new Lazy<HttpClient>(() => new HttpClient());
            if (_user == null)
                return null;
            var response = await httpClient.Value.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint, // https://localhost:44388/connect/token
                ClientId = "ToDoApp_Web",
                ClientSecret = "1q2w3e*",
                UserName = _user.Username,
                Password = _user.Password,
                Scope = "ToDoApp",
            });
            

            //DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.AccessToken);

            if (response.IsError) throw new Exception(response.Error);
            return response;
        }

        private async Task<string> GetAccessToken()
        {
            if(string.IsNullOrEmpty(Token.AccessToken))
            Token.AccessToken = (await GetTokensFromToDoApi()).AccessToken;
            System.Diagnostics.Debug.WriteLine("Access Token: "+Token.AccessToken);
            return Token.AccessToken;
        }

    }
    public class User
    {
        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }
        public User()
        {

        }

        public string Username { get; set; }
        public string Password { get; set; }

    }
    public static class Token {
        public static string AccessToken { get; set; }
    }
}
