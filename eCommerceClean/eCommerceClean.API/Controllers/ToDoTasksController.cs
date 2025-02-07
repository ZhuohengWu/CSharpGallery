using eCommerceClean.Application.Features.ToDo.Create;
using eCommerceClean.Application.Features.ToDo.Delete;
using eCommerceClean.Application.Features.ToDo.Get;
using eCommerceClean.Application.Features.ToDo.GetAll;
using eCommerceClean.Application.Features.ToDo.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eCommerceClean.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoTasksController(ISender sender) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await sender.Send(new GetAllToDoTaskQuery());
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await sender.Send(new GetToDoTaskQuery(id));
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateToDoTask dto)
        {
            var result = await sender.Send(new CreateToDoTaskCommand(dto));
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut()]
        public async Task<IActionResult> Update(UpdateToDoTask dto)
        {
            var result = await sender.Send(new UpdateToDoTaskCommand(dto));
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await sender.Send(new DeleteToDoTaskCommand(new DeleteToDoTask(id)));
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
