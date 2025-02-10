using eCommerceClean.Application.Commons.Responses;
using MediatR;

namespace eCommerceClean.Application.Features.ProductDto.Create
{
    public record class CreateProductCommand(CreateProduct CreateProduct) : IRequest<ServiceResponse<int>>;
    
}
