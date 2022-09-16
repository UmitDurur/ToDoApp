using IdentityServer4.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using System.Text;
using ToDoFrontEnd.Services;
using ToDoFrontEnd.Services.Dtos;
using Volo.Abp.Application.Dtos;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System;

namespace ToDoFrontEnd.Pages
{
    public class ToDoModel : PageModel
    {
        [BindProperty]
        public List<TitleDto> Titles { get; set; } = new List<TitleDto>();
        [BindProperty]
        public string SelectedTitleId { get; set; }

        public async Task OnGet()
        {
            System.Diagnostics.Debug.WriteLine(Request.Cookies["Access-Token"]);
            if (string.IsNullOrEmpty(Request.Cookies["Access-Token"]))
                Response.Redirect("login");


        }

        public async Task<List<ToDoDto>> GetToDos(string titleId)
        {
            var httpService = new HttpService();
            var httpClient = await httpService.GetHttpClientAsync(Request.Cookies["Access-Token"]);

            Dictionary<string, string> query = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(titleId))
                query.Add("TitleId", titleId);
            var uri = QueryHelpers.AddQueryString("api/app/to-do/filtered-list-by-title/", query);
            var response = await httpClient.Value.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ListResultDto<ToDoDto>>(json);
            return result.Items.ToList();

        }

        public async Task OnGetOnClickTitle(string titleId)
        {
            SelectedTitleId = titleId;
        }

        public async Task OnPostToDoDelete(string toDoId)
        {
            var httpService = new HttpService();
            var httpClient = await httpService.GetHttpClientAsync(Request.Cookies["Access-Token"]);
            var response = await httpClient.Value.DeleteAsync("api/app/to-do/" + toDoId);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
        }

        public async Task OnPostToDoAdd(string tododescription, string titleid)
        {
            var httpService = new HttpService();
            var httpClient = await httpService.GetHttpClientAsync(Request.Cookies["Access-Token"]);
            Dictionary<string, string> query = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(tododescription))
                query.Add("Description", tododescription);
            query.Add("TitleId", titleid);

            var json = JsonConvert.SerializeObject(query);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.Value.PostAsync("api/app/to-do/", data);
            try
            {

                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                
            }
        }

        public async Task OnPostToDoEdit(string toDoId,string tododescription,ToDoState edittodostate)
        {
            var httpService = new HttpService();
            var httpClient = await httpService.GetHttpClientAsync(Request.Cookies["Access-Token"]);
            Dictionary<string, string> query = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(tododescription))
                query.Add("Description",tododescription);
            if (edittodostate != 0)
                query.Add("state",((int)edittodostate).ToString());
            var json = JsonConvert.SerializeObject(query);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.Value.PutAsync("api/app/to-do/" + toDoId,data);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
        }



        public async Task<List<TitleDto>> GetTitles()
        {
            var httpService = new HttpService();
            var httpClient = await httpService.GetHttpClientAsync(Request.Cookies["Access-Token"]);
            var response = await httpClient.Value.GetAsync("api/app/to-do/titles");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ListResultDto<TitleDto>>(json);
            if (result?.Items != null)
                Titles = result.Items.ToList();
            if (SelectedTitleId == null)
                SelectedTitleId = Titles.FirstOrDefault().Id.ToString();
            return Titles;
        }
        public string GetUsername()
        {
            return Request.Cookies["Username"];
        }
    }

}
