using IdentityModel;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Security.Claims;
using ToDoFrontEnd.Services;
using ToDoFrontEnd.Services.Dtos;
using Volo.Abp.Account.Web.Pages.Account;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using static IdentityServer4.Models.IdentityResources;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;

namespace ToDoFrontEnd.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public UserDto UserCredentials { get; set; }
        public async Task OnGet()
        {
            if (!string.IsNullOrEmpty(Request.Cookies["Access-Token"]))
                Response.Redirect("todo",true);


        }

        //Just redirect to our index after logging in. 
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

        public async Task OnPost()
        {

            var authService = new AuthService();
            var tokenResponse = await authService.LoginUser(UserCredentials);
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.HttpOnly = true;
            cookieOptions.Expires = DateTime.Now.AddMilliseconds(tokenResponse.ExpiresIn);
            Response.Cookies.Append("Access-Token", tokenResponse.AccessToken,cookieOptions);
            //Response.Cookies.Append("Refresh-Token", tokenResponse.RefreshToken);
            System.Diagnostics.Debug.WriteLine(tokenResponse);
            //if (!string.IsNullOrEmpty(Request.Cookies["Access-Token"]))
            //    Response.RedirectToPage("todo");
            RedirectToPage("todo",true);

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
