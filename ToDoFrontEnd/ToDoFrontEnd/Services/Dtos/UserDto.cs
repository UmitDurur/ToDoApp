namespace ToDoFrontEnd.Services.Dtos
{
    public class UserDto
    {
        public UserDto(string username, string password)
        {
            Username = username;
            Password = password;
        }
        public UserDto()
        {

        }

        public string Username { get; set; }
        public string Password { get; set; }

    }
}
