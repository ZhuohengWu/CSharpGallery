using eCommerceClean.Application.Features.ProductBrandDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceClean.Application.Features.ProductTypeDto.GetAll
{
    public record class GetAllProductType : ProductTypeResponse
    {
        public int Id { get; set; }
    }

}
