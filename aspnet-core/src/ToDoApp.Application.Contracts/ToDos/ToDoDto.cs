using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace ToDoApp.ToDos
{
    public class ToDoDto : AuditedEntityDto<Guid>
    {
        [Required]
        public Guid TitleId { get; set; }
        public string Title { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
        public ToDoState State { get; set; }
    }
}
