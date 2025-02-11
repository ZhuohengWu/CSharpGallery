using eCommerceClean.Application.Commons.Responses;
using MediatR;

namespace eCommerceClean.Application.Features.ProductBrandDto.GetAll;

public record class GetAllProductBrandQuery() : IRequest<ServiceResponse<IEnumerable<GetAllProductBrand>>>;

