using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoFrontEnd.Services;

namespace ToDoFrontEnd.Pages
{
    public class LogoutModel : PageModel
    {
        public async Task OnGet()
        {
            Response.Cookies.Delete("UserName");
            Response.Cookies.Delete("Access-Token");
            Response.Redirect("Login-Callback");
        }
    }
}
