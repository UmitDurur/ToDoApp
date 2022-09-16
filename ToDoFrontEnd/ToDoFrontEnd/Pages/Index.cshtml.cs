using System.Dynamic;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace ToDoFrontEnd.Pages;

public class IndexModel : AbpPageModel
{
    private async Task OnGet()
    {
        Redirect("ToDo");
    }
}