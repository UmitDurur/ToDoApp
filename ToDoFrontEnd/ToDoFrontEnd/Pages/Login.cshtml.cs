using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
            if (!string.IsNullOrWhiteSpace(Request.Cookies["Access-Token"]))
            {
                Response.Redirect("todo");
            }
        }


        public async Task OnPost()
        {
            var authService = new AuthService();
            var tokenResponse = await authService.LoginUser(UserCredentials);

            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.HttpOnly = true;
            cookieOptions.Expires = DateTime.Now.AddMilliseconds(tokenResponse.ExpiresIn);
            cookieOptions.IsEssential = true;

            Response.Cookies.Append("Access-Token", tokenResponse.AccessToken, cookieOptions);
            Response.Cookies.Append("UserName", UserCredentials.Username);

            Response.Redirect("Login-Callback");
        }
    }
}
