using AutoMapper;
using Clean.Application.Commons.Data;
using Clean.Application.Commons.Responses;
using Clean.Domain.Entities;
using MediatR;

namespace Clean.Application.Features.ToDo.Create;

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
