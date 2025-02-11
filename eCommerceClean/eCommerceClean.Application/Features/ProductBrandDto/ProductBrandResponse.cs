using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceClean.Application.Features.ProductBrandDto
{
    public record class ProductBrandResponse
    {
        public string Name { get; set; } = string.Empty;
    }
}
