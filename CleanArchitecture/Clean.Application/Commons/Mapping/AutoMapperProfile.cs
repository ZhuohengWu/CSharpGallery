
using AutoMapper;
using Clean.Application.Features.ToDo.Create;
using Clean.Domain.Entities;

namespace Clean.Application.Commons.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateToDoTask, ToDoTask>();
            CreateMap<ToDoTask, Features.ToDo.Get.GetToDoTask>();
            CreateMap<ToDoTask, Features.ToDo.GetAll.GetToDoTask>();
        }
    }
}
