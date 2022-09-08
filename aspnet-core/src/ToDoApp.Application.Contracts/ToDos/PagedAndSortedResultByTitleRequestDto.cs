using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace ToDoApp.ToDos
{
    public class PagedAndSortedResultByTitleRequestDto : PagedAndSortedResultRequestDto
    {
        public Guid TitleId { get; set; }
    }
}
