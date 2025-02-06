using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceClean.Application.Features.ToDo.Update
{
    public record UpdateToDoTask : ToDoTaskBase
    {
        public Guid Id { get; set; }
    }
}
