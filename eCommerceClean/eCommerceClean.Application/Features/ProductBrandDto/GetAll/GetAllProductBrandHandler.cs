using AutoMapper;
using AutoMapper.QueryableExtensions;
using eCommerceClean.Application.Commons.Data;
using eCommerceClean.Application.Commons.Responses;
using eCommerceClean.Application.Features.ProductDto.Get;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceClean.Application.Features.ProductBrandDto.GetAll
{
    public class GetAllProductTypeHandler(IMapper mapper, IStoreDbContext context) : IRequestHandler<GetAllProductBrandQuery, ServiceResponse<IEnumerable<GetAllProductBrand>>>
    {
        public async Task<ServiceResponse<IEnumerable<GetAllProductBrand>>> Handle(GetAllProductBrandQuery request, CancellationToken cancellationToken)
        {
            var getTasks = await context.ProductBrands
                .ProjectTo<GetAllProductBrand>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return getTasks.Count > 0
                ? ServiceResponse<IEnumerable<GetAllProductBrand>>.Success(getTasks, "Tasks found")
                : new ServiceResponse<IEnumerable<GetAllProductBrand>>([], true, "Null");
        }
    }
}
