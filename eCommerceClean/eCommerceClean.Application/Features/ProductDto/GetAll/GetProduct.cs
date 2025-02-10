using eCommerceClean.Application.Features.ToDo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceClean.Application.Features.ProductDto.GetAll
{
    public record class GetProduct : ToDoTaskBase
    {
        public int Id { get; set; }
    }
}
