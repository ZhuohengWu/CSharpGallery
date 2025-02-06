using AutoMapper;
using Clean.Application.Commons.Data;
using Clean.Application.Commons.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Clean.Application.Features.ToDo.Get
{
    public class GetToDoTaskHandler(IMapper mapper, ICleanDbContext context) : IRequestHandler<GetToDoTaskQuery, ServiceResponse<GetToDoTask>>
    {
        public async Task<ServiceResponse<GetToDoTask>> Handle(GetToDoTaskQuery request, CancellationToken cancellationToken)
        {
            var item = await context.TodoItems.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (item == null)
                return ServiceResponse<GetToDoTask>.Failure("No Task found");

            var mapResponse = mapper.Map<GetToDoTask>(item);
            return ServiceResponse<GetToDoTask>.Success(mapResponse, "Task found");
        }
    }
}
