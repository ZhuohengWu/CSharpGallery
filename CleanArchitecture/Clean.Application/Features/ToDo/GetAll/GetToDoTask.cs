using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Application.Features.ToDo.GetAll
{
    public record GetToDoTask : ToDoTaskBase
    {
        public Guid Id { get; set; }
    }
}
