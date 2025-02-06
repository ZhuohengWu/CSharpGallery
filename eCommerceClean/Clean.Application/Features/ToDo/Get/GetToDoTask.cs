using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceClean.Application.Features.ToDo.Get
{
    public record GetToDoTask : ToDoTaskBase
    {
        public Guid Id { get; set; }
    }
}
