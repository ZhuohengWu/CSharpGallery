using eCommerceClean.Application.Commons.Responses;
using MediatR;

namespace eCommerceClean.Application.Features.ProductDto.GetAll
{
    public record class GetAllProductQuery : IRequest<ServiceResponse<IEnumerable<GetProduct>>>;
    
}
