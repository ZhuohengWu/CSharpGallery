using Clean.Application.Commons.Responses;
using MediatR;

namespace Clean.Application.Features.ToDo.Update;

public record UpdateToDoTaskCommand(UpdateToDoTask UpdateToDo) : IRequest<ServiceResponse<Guid>>;
