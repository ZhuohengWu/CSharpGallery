using AutoMapper;
using eCommerceClean.Application.Commons.Data;
using eCommerceClean.Application.Commons.Responses;
using eCommerceClean.Domain.Entities;
using MediatR;

namespace eCommerceClean.Application.Features.ToDo.Create;

public class CreateToDoTaskHandler(ICleanDbContext context, IMapper mapper) : IRequestHandler<CreateToDoTaskCommand, ServiceResponse<Guid>>
{
    public async Task<ServiceResponse<Guid>> Handle(CreateToDoTaskCommand request, CancellationToken cancellationToken)
    {
        var mapDto = mapper.Map<ToDoTask>(request.CreateToDo);
        if (mapDto == null)
            return ServiceResponse<Guid>.Failure("Invalid request");

        context.TodoItems.Add(mapDto);
        await context.SaveChangesAsync(cancellationToken);
        return ServiceResponse<Guid>.Success(Guid.Empty, "Task created");
    }
}
