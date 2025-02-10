using eCommerceClean.Application.Commons.Responses;
using MediatR;
namespace eCommerceClean.Application.Features.ProductDto.Get;

public record class GetProductQuery(int Id) : IRequest<ServiceResponse<GetProduct>>;

