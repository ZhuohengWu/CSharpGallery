using eCommerceClean.Application.Commons.Responses;
using MediatR;

namespace eCommerceClean.Application.Features.ToDo.GetAll
{
    public record GetAllToDoTaskQuery : IRequest<ServiceResponse<IEnumerable<GetToDoTask>>>;
}
