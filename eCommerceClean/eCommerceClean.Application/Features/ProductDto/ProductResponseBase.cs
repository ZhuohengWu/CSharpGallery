using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceClean.Application.Features.ProductDto
{
    public record class ProductResponseBase
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string PictureUrl { get; set; } = string.Empty;
        public string ProductType { get; set; } = string.Empty;
        public string ProductBrand { get; set; } = string.Empty;
    }
}
