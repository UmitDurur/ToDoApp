using IdentityModel.Client;
using IdentityServer4.Models;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using ToDoFrontEnd.Services.Dtos;
using Volo.Abp.IdentityServer.Clients;

namespace ToDoFrontEnd.Services
{
    public class AuthService
    {
        private static async Task<TokenResponse> GetTokensFromToDoApi(UserDto user)
        {
            var authority = "https://localhost:44352/";
            var discoveryCache = new DiscoveryCache(authority);
            var disco = await discoveryCache.GetAsync();

            var httpClient = new Lazy<HttpClient>(() => new HttpClient());
            if (user == null)
                return null;
            var response = await httpClient.Value.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint, // https://localhost:44388/connect/token
                ClientId = "ToDoApp_Web",
                ClientSecret = "1q2w3e*",
                UserName = user.Username,
                Password = user.Password,
                Scope = "ToDoApp",
            });
            httpClient.Value.DefaultRequestHeaders.Add("grant_type", GrantTypes.ResourceOwnerPasswordAndClientCredentials);



            //DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.AccessToken);

            if (response.IsError) throw new Exception(response.Error);
            return response;
        }
        public async Task<TokenResponse> LoginUser(UserDto _user)
        {
            var accessToken = (await GetTokensFromToDoApi(_user));
            return accessToken;
        }
    }
}
