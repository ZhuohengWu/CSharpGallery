using AutoMapper;
using eCommerceClean.Application.Commons.Data;
using eCommerceClean.Application.Commons.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eCommerceClean.Application.Features.ProductDto.Get
{
    public class GetProductHandler(IMapper mapper, IStoreDbContext context) : IRequestHandler<GetProductQuery, ServiceResponse<GetProduct>>
    {
        public async Task<ServiceResponse<GetProduct>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var item = await context.Products.AsNoTracking()
                // todo: (when large data sets) remove following 2 Include to simplify the sql query
                // instead, automap the ids in AutoMapperProfile,
                // frontend will use separate query to get types and maps it with product.
                .Include(p => p.ProductBrand) 
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (item == null)
                return ServiceResponse<GetProduct>.Failure(ResponseCode.NotFound, "No Product found");

            var result = mapper.Map<GetProduct>(item);
            return ServiceResponse<GetProduct>.Success(result, "Product found");
        }
    }
}
