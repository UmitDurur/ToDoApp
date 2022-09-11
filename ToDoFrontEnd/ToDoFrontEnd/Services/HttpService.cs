using IdentityModel.Client;
using IdentityServer4.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Http;
using Serilog;
using System.Net.Http.Headers;
using Volo.Abp.DependencyInjection;
using ToDoFrontEnd.Services.Dtos;

namespace ToDoFrontEnd.Services
{
    public class HttpService
    {
        public async Task<Lazy<HttpClient>> GetHttpClientAsync(string accessToken)
        {
            var client = new Lazy<HttpClient>(() => new HttpClient());
            client.Value.SetBearerToken(accessToken);
            client.Value.BaseAddress = new Uri("https://localhost:44352/"); //
            return await Task.FromResult(client);
        }

    }
}
