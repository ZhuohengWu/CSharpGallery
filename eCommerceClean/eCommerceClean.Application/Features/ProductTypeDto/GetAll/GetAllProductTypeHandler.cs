using AutoMapper;
using AutoMapper.QueryableExtensions;
using eCommerceClean.Application.Commons.Data;
using eCommerceClean.Application.Commons.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eCommerceClean.Application.Features.ProductTypeDto.GetAll
{
    public class GetAllProductTypeHandler(IMapper mapper, IStoreDbContext context) : IRequestHandler<GetAllProductTypeQuery, ServiceResponse<IEnumerable<GetAllProductType>>>
    {
        public async Task<ServiceResponse<IEnumerable<GetAllProductType>>> Handle(GetAllProductTypeQuery request, CancellationToken cancellationToken)
        {
            var getTasks = await context.ProductTypes
                .ProjectTo<GetAllProductType>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return getTasks.Count > 0
                ? ServiceResponse<IEnumerable<GetAllProductType>>.Success(getTasks, "Tasks found")
                : new ServiceResponse<IEnumerable<GetAllProductType>>([], true, "Null");
        }
    }
}
