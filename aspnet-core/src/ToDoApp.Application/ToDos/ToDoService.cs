using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using System.Linq.Dynamic.Core;
using Volo.Abp.Application.Services;
using Volo.Abp.ObjectMapping;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Users;

namespace ToDoApp.ToDos
{

    public class ToDoService : ToDoAppAppService, IToDoService
    {
        private readonly IRepository<ToDo, Guid> _toDoRepository;
        private readonly IRepository<ToDoTitle, Guid> _titleRepository;
        private readonly ICurrentUser _currentUser;
        public ToDoService(IRepository<ToDo, Guid> todoRepository, IRepository<ToDoTitle, Guid> titleRepository,ICurrentUser currentUser)
        {
            _toDoRepository = todoRepository;
            _titleRepository = titleRepository;
            _currentUser = currentUser;
        }

        [Authorize("ToDoApp.ToDoCreation")]
        public async Task CreateAsync(CreateUpdateToDoDto toDoDto)
        {
            var item = ObjectMapper.Map<CreateUpdateToDoDto, ToDo>(toDoDto);
            await _toDoRepository.InsertAsync(item);
        }

        [Authorize("ToDoApp.ToDoDeletion")]
        public async Task DeleteAsync(Guid id)
        {
            var toDo = await _toDoRepository.FirstOrDefaultAsync(t=>t.Id==id);
            if(toDo!=null)
                await _toDoRepository.DeleteAsync(toDo);
        }

        public async Task<PagedResultDto<ToDoDto>> GetFilteredListAsync(PagedAndSortedResultRequestDto input)
        {
            var queryable = await _toDoRepository
               .WithDetailsAsync(x => x.Title);

            queryable = queryable.Where(t=>t.CreatorId==_currentUser.Id)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .OrderBy(input.Sorting ?? nameof(ToDo.Description));

            var todos = await AsyncExecuter.ToListAsync(queryable);

            int count = await _toDoRepository.CountAsync(t => t.CreatorId == _currentUser.Id);

            return new PagedResultDto<ToDoDto>(
                count,
                ObjectMapper.Map<List<ToDo>, List<ToDoDto>>(todos)
                );
        }

        public async Task<PagedResultDto<ToDoDto>> GetFilteredListByTitleAsync(PagedAndSortedResultByTitleRequestDto input)
        {
            var queryable = await _toDoRepository.WithDetailsAsync(x => x.Title);

            queryable = queryable.Where(t=>
                                    t.CreatorId==_currentUser.Id 
                                    && t.TitleId==input.TitleId)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .OrderBy(input.Sorting ?? nameof(ToDo.Description));

            var todos = await AsyncExecuter.ToListAsync(queryable);

            int count = await _toDoRepository.CountAsync(t=>t.CreatorId==_currentUser.Id && t.TitleId==input.TitleId);

            return new PagedResultDto<ToDoDto>(
                count,
                ObjectMapper.Map<List<ToDo>, List<ToDoDto>>(todos)
                );
        }
        [Authorize]
        public async Task<ListResultDto<ToDoTitleLookupDto>> GetTitlesAsync()
        {
            var titles = await _titleRepository.GetListAsync();
            return new ListResultDto<ToDoTitleLookupDto>(
            ObjectMapper
            .Map<List<ToDoTitle>, List<ToDoTitleLookupDto>>
           (titles)
            );
        }


        [Authorize("ToDoApp.GetToDo")]
        public async Task<ToDoDto> Get(Guid id)
        {
            var item = await _toDoRepository.FirstOrDefaultAsync(t => t.Id == id);
            return ObjectMapper.Map<ToDo,ToDoDto>(item);   
        }
    }
}
