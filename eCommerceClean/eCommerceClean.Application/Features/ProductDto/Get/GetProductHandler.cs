using AutoMapper;
using eCommerceClean.Application.Commons.Data;
using eCommerceClean.Application.Commons.Responses;
using eCommerceClean.Application.Features.ToDo.Get;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceClean.Application.Features.ProductDto.Get
{
    public class GetProductHandler(IMapper mapper, IStoreDbContext context) : IRequestHandler<GetProductQuery, ServiceResponse<GetProduct>>
    {
        public async Task<ServiceResponse<GetProduct>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var item = await context.Products.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (item == null)
                return ServiceResponse<GetProduct>.Failure("No Task found");

            var mapResponse = mapper.Map<GetProduct>(item);
            return ServiceResponse<GetProduct>.Success(mapResponse, "Task found");
        }
    }
}
