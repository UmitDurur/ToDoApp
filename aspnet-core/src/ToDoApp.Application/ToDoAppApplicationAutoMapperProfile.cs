using AutoMapper;
using ToDoApp.ToDos;

namespace ToDoApp;

public class ToDoAppApplicationAutoMapperProfile : Profile
{
    public ToDoAppApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<ToDo, ToDoDto>();
        CreateMap<ToDoDto, ToDo>();

        CreateMap<CreateUpdateToDoDto, ToDo>();
        CreateMap<ToDo,CreateUpdateToDoDto>();
        CreateMap<CreateUpdateToDoDto, ToDoDto>();
        CreateMap<ToDoDto, CreateUpdateToDoDto>();

        CreateMap<ToDoTitleLookupDto, ToDoTitle>();
        CreateMap<ToDoTitle, ToDoTitleLookupDto>();
    }
}
