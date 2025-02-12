
using AutoMapper;
using eCommerceClean.Application.Features.ProductDto;
using eCommerceClean.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace eCommerceClean.Application.Commons.Mapping
{
    public class ProductImageUrlResolver(IConfiguration Config) : IValueResolver<Product, ProductResponse, string>
    {
        public string Resolve(Product source, ProductResponse destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return Config["ApiUrl"] + source.PictureUrl;
            }

            return string.Empty;
        }
    }
}
