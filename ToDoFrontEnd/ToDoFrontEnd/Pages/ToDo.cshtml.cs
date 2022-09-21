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
using NUglify.JavaScript.Syntax;

namespace ToDoFrontEnd.Pages
{
    public class ToDoModel : PageModel
    {
        [BindProperty]
        public List<TitleDto> Titles { get; set; } = new List<TitleDto>();
        [BindProperty]
        public string SelectedTitleId { get; set; }

        private readonly HttpService _httpService;
        public ToDoModel()
        {
            _httpService = new HttpService();
        }

        public async Task OnGet()
        {
            System.Diagnostics.Debug.WriteLine(Request.Cookies["Access-Token"]);
            if (string.IsNullOrEmpty(Request.Cookies["Access-Token"]))
                Response.Redirect("login");


        }

        public async Task<List<ToDoDto>> GetToDos(string titleId)
        {
            Dictionary<string, string> query = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(titleId))
                query.Add("TitleId", titleId);
            var uri = QueryHelpers.AddQueryString("to-do/filtered-list-by-title/", query);

            var requestDto = new HttpRequestDto
            {
                AccessToken = Request.Cookies["Access-Token"],
                HttpMethod = HttpMethod.Get,
                Uri = uri
            };

            var json = await _httpService.ExecuteAsync(requestDto);
            var result = JsonConvert.DeserializeObject<ListResultDto<ToDoDto>>(json);
            return result.Items.ToList();
        }

        public async Task OnGetOnClickTitle(string titleId)
        {
            SelectedTitleId = titleId;
        }

        public async Task OnPostToDoDelete(string toDoId)
        {
            var uri = "to-do/" + toDoId;

            var requestDto = new HttpRequestDto
            {
                AccessToken = Request.Cookies["Access-Token"],
                HttpMethod = HttpMethod.Delete,
                Uri = uri
            };

            try
            {
                await _httpService.ExecuteAsync(requestDto);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("OnPostToDoDelete Error: " + ex.Message);
            }
        }

        public async Task OnPostToDoAdd(string tododescription, string titleid)
        {

            if (!string.IsNullOrEmpty(tododescription))
            {
                Dictionary<string, string> query = new Dictionary<string, string>();
                query.Add("Description", tododescription);
                query.Add("TitleId", titleid);

                var content = _httpService.CreateStringContent(query);
                var requestDto = new HttpRequestDto
                {
                    AccessToken = Request.Cookies["Access-Token"],
                    HttpMethod = HttpMethod.Post,
                    Uri = "to-do/",
                    StringContent = content
                };

                try
                {
                    await _httpService.ExecuteAsync(requestDto);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("OnPostToDoAdd Error: " + ex.Message);
                }
            }
        }

        public async Task OnPostToDoEdit(string toDoId, string tododescription, ToDoState edittodostate)
        {
            if (!string.IsNullOrWhiteSpace(toDoId))
            {
                Dictionary<string, string> query = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(tododescription))
                    query.Add("Description", tododescription);
                if (edittodostate != 0)
                    query.Add("state", ((int)edittodostate).ToString());

                var content = _httpService.CreateStringContent(query);

                var uri = "to-do/" + toDoId;

                var request = new HttpRequestDto
                {
                    AccessToken = Request.Cookies["Access-Token"],
                    HttpMethod = HttpMethod.Put,
                    Uri = uri,
                    StringContent = content
                };
                try
                {
                    await _httpService.ExecuteAsync(request);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("OnPostToDoEdit Error: " + ex.Message);
                }
            }
        }



        public async Task<List<TitleDto>> GetTitles()
        {
            var request = new HttpRequestDto
            {
                AccessToken = Request.Cookies["Access-Token"],
                HttpMethod = HttpMethod.Get,
                Uri = "to-do/titles"
            };


            try
            {
                var json = await _httpService.ExecuteAsync(request);
                var result = JsonConvert.DeserializeObject<ListResultDto<TitleDto>>(json);
                if (result?.Items != null)
                    Titles = result.Items.ToList();
                if (SelectedTitleId == null)
                    SelectedTitleId = Titles.FirstOrDefault().Id.ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("GetTitles Error: "+ ex.Message);
            }

            return Titles;
        }
        public string GetUsername()
        {
            return Request.Cookies["Username"];
        }
    }

}
