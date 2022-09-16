using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ToDoFrontEnd.Pages
{
    public class Login_CallbackModel : PageModel
    {
        public void OnGet()
        {
            if (!string.IsNullOrWhiteSpace(Request.Cookies["Access-Token"]))
            {
                Response.Redirect("todo");
            }
            else Response.Redirect("login");
        }
    }
}
