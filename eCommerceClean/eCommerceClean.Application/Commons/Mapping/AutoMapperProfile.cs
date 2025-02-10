
using AutoMapper;
using eCommerceClean.Application.Features.ProductDto.Create;
//using eCommerceClean.Application.Features.ToDo.Create;
//using eCommerceClean.Application.Features.ToDo.Update;
using eCommerceClean.Domain.Entities;

namespace eCommerceClean.Application.Commons.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<CreateToDoTask, ToDoTask>();
            //CreateMap<ToDoTask, Features.ToDo.Get.GetToDoTask>();
            //CreateMap<ToDoTask, Features.ToDo.GetAll.GetToDoTask>();
            //CreateMap<UpdateToDoTask, ToDoTask>();


            CreateMap<CreateProduct, Product>();
            CreateMap<Product, Features.ProductDto.Get.GetProduct>();
            CreateMap<Product, Features.ProductDto.GetAll.GetProduct>();
        }
    }
}
