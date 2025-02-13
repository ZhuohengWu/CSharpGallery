using AutoMapper;
using AutoMapper.QueryableExtensions;
using eCommerceClean.Application.Commons.Data;
using eCommerceClean.Application.Commons.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eCommerceClean.Application.Features.ProductDto.GetAll
{
    public class GetAllProductHandler(IStoreDbContext context, IMapper mapper) : IRequestHandler<GetAllProductQuery, ServiceResponse<IEnumerable<GetProduct>>>
    {
        public async Task<ServiceResponse<IEnumerable<GetProduct>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var items = await context.Products
                .AsNoTracking().Include(p => p.ProductBrand).Include(p => p.ProductType)
                .ToListAsync(cancellationToken);

            var results = mapper.Map<IEnumerable<GetProduct>>(items);

            return results.Count() > 0
                ? ServiceResponse<IEnumerable<GetProduct>>.Success(results, "Products found")
                : new ServiceResponse<IEnumerable<GetProduct>>(Enumerable.Empty<GetProduct>(), true, ResponseCode.Success, "No Products found");
        }
    }
}
