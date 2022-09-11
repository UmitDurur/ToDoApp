using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ToDoFrontEnd.Services;
using ToDoFrontEnd.Services.Dtos;
using Volo.Abp.Application.Dtos;

namespace ToDoFrontEnd.Pages.Partials
{
    public class ToDoDatatableModel : PageModel
    {
        [BindProperty]
        public List<ToDoDto> ToDos { get; set; }=new List<ToDoDto>();
        public async Task OnGet()
        {
            var httpService = new HttpService();
            var httpClient = await httpService.GetHttpClientAsync(Request.Cookies["Access-Token"]);
            var response = await httpClient.Value.GetAsync("api/app/to-do/filtered-list-by-title/1C2FCCE8-FECE-6260-3AF1-3A061148BA3B");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ListResultDto<ToDoDto>>(json);
            ToDos = result.Items.ToList();

        }
    }
}
