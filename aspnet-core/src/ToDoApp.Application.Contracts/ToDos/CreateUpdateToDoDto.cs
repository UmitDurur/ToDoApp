using System;

namespace ToDoApp.ToDos
{
    public class CreateUpdateToDoDto
    {
        public Guid TitleId { get; set; }
        public string Description { get; set; }
        public ToDoState State { get; set; }
    }
}