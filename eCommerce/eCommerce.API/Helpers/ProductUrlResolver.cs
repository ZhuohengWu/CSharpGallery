using AutoMapper;
using eCommerce.API.Dtos;
using eCommerce.Core.Entities;

namespace eCommerce.API.Helpers
{
    public class ProductUrlResolver(IConfiguration Config) : IValueResolver<Product, ProductToReturnDto, string>
    {
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl)) 
            { 
                return Config["ApiUrl"] + source.PictureUrl;
            }

            return string.Empty;
        }
    }
}
