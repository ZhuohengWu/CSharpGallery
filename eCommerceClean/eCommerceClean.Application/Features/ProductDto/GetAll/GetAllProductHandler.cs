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
            var getTasks = await context.Products
                .AsNoTracking()
                .ProjectTo<GetProduct>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return getTasks.Count > 0
                ? ServiceResponse<IEnumerable<GetProduct>>.Success(getTasks, "Tasks found")
                : new ServiceResponse<IEnumerable<GetProduct>>([], true, "Null");
        }
    }
}
