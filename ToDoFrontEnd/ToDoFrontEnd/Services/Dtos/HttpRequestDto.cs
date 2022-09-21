namespace ToDoFrontEnd.Services.Dtos
{
    public class HttpRequestDto
    {
        public HttpMethod HttpMethod { get; set; }
        public string AccessToken { get; set; }
        public string Uri { get; set; }
        public StringContent? StringContent { get; set; }
    }
}
