using System.ComponentModel.DataAnnotations;

namespace ToDoFrontEnd.Services.Dtos
{
    public class ToDoDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ToDoState State { get; set; }
    }
}
