using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace ToDoApp.ToDos
{
    public class ToDo : AuditedAggregateRoot<Guid>
    {
        public ToDoTitle Title { get; set; }
        public Guid TitleId { get; set; }
        public string Description { get; set; }
        public ToDoState State { get; set; }
    }
}
