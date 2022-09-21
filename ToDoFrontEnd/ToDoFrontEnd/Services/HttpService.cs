using IdentityModel.Client;
using IdentityServer4.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Http;
using Serilog;
using System.Net.Http.Headers;
using Volo.Abp.DependencyInjection;
using ToDoFrontEnd.Services.Dtos;
using System;
using Microsoft.CodeAnalysis.Operations;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Text;

namespace ToDoFrontEnd.Services
{
    public class HttpService
    {
        
        public HttpService()
        {

        }

        public async Task<Lazy<HttpClient>> GetHttpClientAsync(string accessToken)
        {
            var client = new Lazy<HttpClient>(() => new HttpClient());
            client.Value.SetBearerToken(accessToken);
            client.Value.BaseAddress = new Uri("https://localhost:44352/api/app/"); //
            return await Task.FromResult(client);
        }

        public async Task<string> ExecuteAsync(HttpRequestDto request) {
            var httpClient = await GetHttpClientAsync(request.AccessToken);


            var httpRequestMessage=new HttpRequestMessage(request.HttpMethod, request.Uri);
            if (request.StringContent != null)
                httpRequestMessage.Content = request.StringContent;

            var response = await httpClient.Value.SendAsync(httpRequestMessage);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine("Execute http request error: "+ex.Message);
            }
            return await response.Content.ReadAsStringAsync(); ;
        }

        public StringContent CreateStringContent(Dictionary<string, string> kvp) {
            var json = JsonConvert.SerializeObject(kvp);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            return data;
        }
    }
}
