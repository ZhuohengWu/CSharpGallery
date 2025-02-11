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
            var item = await context.Products
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (item == null)
                return ServiceResponse<GetProduct>.Failure("No Task found");

            var mapResponse = mapper.Map<GetProduct>(item);
            return ServiceResponse<GetProduct>.Success(mapResponse, "Task found");
        }
    }
}
