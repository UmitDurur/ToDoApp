using Microsoft.AspNetCore.Authentication;
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
        [BindProperty]
        public List<TitleDto> Titles { get; set; } = new List<TitleDto>();

        public async Task OnGet()
        {
            if (string.IsNullOrEmpty(Request.Cookies["Access-Token"]))
                Response.Redirect("login",true);
            System.Diagnostics.Debug.WriteLine(Request.Cookies["Access-Token"]);
            if (this.User.Identity.IsAuthenticated)
                System.Diagnostics.Debug.WriteLine("Authenticated");


            var httpService = new HttpService();
            var httpClient = await httpService.GetHttpClientAsync(Request.Cookies["Access-Token"]);
            var response = await httpClient.Value.GetAsync("api/app/to-do/titles");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ListResultDto<TitleDto>>(json);
            if (result?.Items != null)
                Titles = result.Items.ToList();
            foreach (var item in Titles)
            {
                System.Diagnostics.Debug.WriteLine(item.Title);
            }
        }


        public async Task OnPost()
        {

        }
    }

}
