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
    }
}
