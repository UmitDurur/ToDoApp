using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.ToDos;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace ToDoApp.Data
{
    public class ToDoDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<ToDoTitle,Guid> _titleRepository;
        public ToDoDataSeedContributor(IRepository<ToDoTitle,Guid> titleRepository)
        {
            _titleRepository = titleRepository;
        }
        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _titleRepository.CountAsync() > 0)
            { return; }

            ToDoTitle title1 = new ToDoTitle();
            title1.Title = "Personel";
            ToDoTitle title2=new ToDoTitle();
            title2.Title = "Work";

            await _titleRepository.
                InsertManyAsync(new[] { title1, title2 });

        }
    }
}
