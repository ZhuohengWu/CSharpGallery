using Clean.Application.Features.ToDo.Create;
using Clean.Application.Features.ToDo.Get;
using Clean.Application.Features.ToDo.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Clean.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoTasksController(ISender sender) : ControllerBase
    {
        // GET: api/<ToDoTasksController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await sender.Send(new GetAllToDoTaskQuery());
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        // GET api/<ToDoTasksController>/5
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await sender.Send(new GetToDoTaskQuery(id));
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        // POST api/<ToDoTasksController>
        [HttpPost]
        public async Task<IActionResult> Create(CreateToDoTask dto)
        {
            var result = await sender.Send(new CreateToDoTaskCommand(dto));
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        // PUT api/<ToDoTasksController>/5
        [HttpPut("{id}")]
        public void Update(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ToDoTasksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
