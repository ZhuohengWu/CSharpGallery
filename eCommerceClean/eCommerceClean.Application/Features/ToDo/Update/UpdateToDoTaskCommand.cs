using eCommerceClean.Application.Commons.Responses;
using MediatR;

namespace eCommerceClean.Application.Features.ToDo.Update;

public record UpdateToDoTaskCommand(UpdateToDoTask UpdateToDo) : IRequest<ServiceResponse<Guid>>;
