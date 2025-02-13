using AutoMapper;
using AutoMapper.QueryableExtensions;
using eCommerceClean.Application.Commons.Data;
using eCommerceClean.Application.Commons.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eCommerceClean.Application.Features.ProductTypeDto.GetAll
{
    public class GetAllProductTypeHandler(IMapper mapper, IStoreDbContext context) : IRequestHandler<GetAllProductTypeQuery, ServiceResponse<IEnumerable<GetAllProductType>>>
    {
        public async Task<ServiceResponse<IEnumerable<GetAllProductType>>> Handle(GetAllProductTypeQuery request, CancellationToken cancellationToken)
        {
            var results = await context.ProductTypes
                .ProjectTo<GetAllProductType>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return results is not null && results.Count > 0
                ? ServiceResponse<IEnumerable<GetAllProductType>>.Success(results, "Product types found")
                : new ServiceResponse<IEnumerable<GetAllProductType>>(Enumerable.Empty<GetAllProductType>(), true, ResponseCode.Success, "No product types found");
        }
    }
}
