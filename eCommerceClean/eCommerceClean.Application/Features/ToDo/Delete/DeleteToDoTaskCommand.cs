using eCommerceClean.Application.Commons.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceClean.Application.Features.ToDo.Delete;

public record DeleteToDoTaskCommand(DeleteToDoTask DeleteToDo) : IRequest<ServiceResponse<Guid>>;
