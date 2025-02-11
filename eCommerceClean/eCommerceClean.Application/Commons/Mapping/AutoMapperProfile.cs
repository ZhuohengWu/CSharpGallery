
using AutoMapper;
using eCommerceClean.Application.Features.ProductDto.Create;
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

            CreateMap<Product, Features.ProductDto.Get.GetProduct>()
                .ForMember(response => response.ProductType, opt => opt.MapFrom(entity => entity.ProductType.Name))
                .ForMember(response => response.ProductBrand, opt => opt.MapFrom(entity => entity.ProductBrand.Name));

            CreateMap<Product, Features.ProductDto.GetAll.GetProduct>()
                .ForMember(response => response.ProductType, opt => opt.MapFrom(entity => entity.ProductType.Name))
                .ForMember(response => response.ProductBrand, opt => opt.MapFrom(entity => entity.ProductBrand.Name));

            CreateMap<ProductBrand, Features.ProductBrandDto.GetAll.GetAllProductBrand>();
            CreateMap<ProductType, Features.ProductTypeDto.GetAll.GetAllProductType>();
        }
    }
}
