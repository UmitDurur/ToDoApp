using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ToDoApp.ToDos
{
    public interface IToDoService : IApplicationService
    {
        Task<PagedResultDto<ToDoDto>> GetFilteredListAsync(PagedAndSortedResultRequestDto input);
        Task<PagedResultDto<ToDoDto>> GetFilteredListByTitleAsync(PagedAndSortedResultByTitleRequestDto input);

        Task CreateAsync(CreateUpdateToDoDto toDoDto);

        Task<ListResultDto<ToDoTitleLookupDto>> GetTitlesAsync();

        Task DeleteAsync(Guid id);

        Task<ToDoDto> Get(Guid id);

        Task UpdateAsync(Guid id, CreateUpdateToDoDto update);

    }
}
