using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Security.Claims;
using ToDoFrontEnd.Services;
using ToDoFrontEnd.Services.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace ToDoFrontEnd.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public User User { get; set; }
        public async Task OnGet()
        {
            if (!string.IsNullOrEmpty(Token.AccessToken))
                Response.Redirect("/todo");
            //const bool setBearerToken = true;

            //var httpService = new HttpService("admin", "1q2w3E*");
            //var httpClient = await httpService.GetHttpClientAsync(setBearerToken);

            //var response = await httpClient.Value.GetAsync("https://localhost:44352/api/app/to-do/titles");
            //response.EnsureSuccessStatusCode();


            //var json = await response.Content.ReadAsStringAsync();

            //var books = JsonConvert.DeserializeObject<ListResultDto<TitleDto>>(json);

            //if (books?.Items != null)
            //    foreach (var book in books.Items)
            //        System.Diagnostics.Debug.WriteLine(book.Title);

        }

        public async Task OnPost()
        {
            // if setBearerToken = false, should throw HttpRequestException: 'Response status code does not indicate success: 401 (Unauthorized).'
            // if setBearerToken = true, API should be called an list of books should be returned
            const bool setBearerToken = true;

            var httpService = new HttpService(User.Username, User.Password);
            var httpClient = await httpService.GetHttpClientAsync(setBearerToken);
            if (!string.IsNullOrEmpty(Token.AccessToken))
                Response.Redirect("/todo");
            //var response = await httpClient.Value.GetAsync("https://localhost:44352/api/app/to-do/titles");
            //response.EnsureSuccessStatusCode();


            //var json = await response.Content.ReadAsStringAsync();

            //var books = JsonConvert.DeserializeObject<ListResultDto<TitleDto>>(json);

            //if (books?.Items != null)
            //    foreach (var book in books.Items)
            //        System.Diagnostics.Debug.WriteLine(book.Title);
        }
    }
}
