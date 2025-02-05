using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Application.Features.ToDo.Create
{
    public record CreateToDoTaskCommand(CreateToDoTask CreateToDo) : IRequest<Guid>
    {
    }
}
