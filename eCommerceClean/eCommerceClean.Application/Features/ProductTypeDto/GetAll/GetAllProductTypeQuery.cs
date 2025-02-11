using eCommerceClean.Application.Commons.Responses;
using MediatR;

namespace eCommerceClean.Application.Features.ProductTypeDto.GetAll;

public record class GetAllProductTypeQuery() : IRequest<ServiceResponse<IEnumerable<GetAllProductType>>>;

