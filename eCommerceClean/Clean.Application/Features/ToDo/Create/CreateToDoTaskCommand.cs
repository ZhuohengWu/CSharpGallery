using eCommerceClean.Application.Commons.Responses;
using MediatR;

namespace eCommerceClean.Application.Features.ToDo.Create;

public record class CreateToDoTaskCommand(CreateToDoTask CreateToDo) : IRequest<ServiceResponse<Guid>>;
