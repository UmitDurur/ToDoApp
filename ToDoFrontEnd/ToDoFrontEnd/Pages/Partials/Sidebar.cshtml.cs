using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using ToDoFrontEnd.Services;
using ToDoFrontEnd.Services.Dtos;
using Volo.Abp.Application.Dtos;

namespace ToDoFrontEnd.Pages.Partials
{
  
    public class SidebarModel : PageModel
    {
        [BindProperty]
        public List<TitleDto> Titles { get; set; }=new List<TitleDto>();
        public async Task OnGet()
        {
            var httpService = new HttpService();
            var httpClient = await httpService.GetHttpClientAsync(true);
            var response = await httpClient.Value.GetAsync("https://localhost:44352/api/app/to-do/titles");
            response.EnsureSuccessStatusCode();


            var json = await response.Content.ReadAsStringAsync();

            var books = JsonConvert.DeserializeObject<ListResultDto<TitleDto>>(json);

            if (books?.Items != null)
                Titles = books.Items.ToList();
            ViewData["Titles"] = Titles;
        }
    }
}
