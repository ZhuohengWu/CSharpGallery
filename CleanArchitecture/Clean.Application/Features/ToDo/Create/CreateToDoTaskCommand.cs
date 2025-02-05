using Clean.Application.Commons.Responses;
using MediatR;

namespace Clean.Application.Features.ToDo.Create;

public record class CreateToDoTaskCommand(CreateToDoTask CreateToDo) : IRequest<ServiceResponse<Guid>>;
