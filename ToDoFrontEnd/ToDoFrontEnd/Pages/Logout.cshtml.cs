using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoFrontEnd.Services;

namespace ToDoFrontEnd.Pages
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
            Token.AccessToken = String.Empty;

            if (string.IsNullOrEmpty(Token.AccessToken))
                Response.Redirect("/login");
        }
    }
}
