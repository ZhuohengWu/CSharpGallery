using AutoMapper;
using eCommerceClean.Application.Commons.Data;
using eCommerceClean.Application.Commons.Responses;
using eCommerceClean.Domain.Entities;
using MediatR;

namespace eCommerceClean.Application.Features.ProductDto.Create
{
    internal class CreateProductHandler(IStoreDbContext context, IMapper mapper) : IRequestHandler<CreateProductCommand, ServiceResponse<int>>
    {
        public async Task<ServiceResponse<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var mapDto = mapper.Map<Product>(request.CreateProduct);
            if (mapDto == null)
                return ServiceResponse<int>.Failure(ResponseCode.BadRequest, "Invalid request");

            context.Products.Add(mapDto);
            await context.SaveChangesAsync(cancellationToken);
            return ServiceResponse<int>.Success(default, "Product created");
        }
    }
}
