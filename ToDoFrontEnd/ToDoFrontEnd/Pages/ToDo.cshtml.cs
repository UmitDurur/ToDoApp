using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ToDoFrontEnd.Services;
using ToDoFrontEnd.Services.Dtos;
using Volo.Abp.Application.Dtos;

namespace ToDoFrontEnd.Pages
{
    public class ToDoModel : PageModel
    {
        public async void OnGet()
        {
            //if (string.IsNullOrEmpty(Token.AccessToken))
            //    Response.Redirect("/login",true);

        }
    }
}
