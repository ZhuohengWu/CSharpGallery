using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceClean.Application.Features.ProductTypeDto
{
    public record class ProductTypeResponse
    {
        public string Name { get; set; } = string.Empty;
    }
}
