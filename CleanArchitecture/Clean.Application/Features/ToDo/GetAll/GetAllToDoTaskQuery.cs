using Clean.Application.Commons.Responses;
using MediatR;

namespace Clean.Application.Features.ToDo.GetAll
{
    public record GetAllToDoTaskQuery : IRequest<ServiceResponse<IEnumerable<GetToDoTask>>>;
}
