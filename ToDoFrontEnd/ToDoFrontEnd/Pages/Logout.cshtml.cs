using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoFrontEnd.Services;

namespace ToDoFrontEnd.Pages
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
            Response.Cookies.Append("Access-Token", "");
            if (string.IsNullOrEmpty(Request.Cookies["Access-Token"]))
                Response.Redirect("login",true);
        }
    }
}
