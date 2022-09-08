using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace ToDoApp.ToDos
{
    public class ToDoTitle: AuditedAggregateRoot<Guid>
    {
        public string Title { get; set; }
    }
}
