using eCommerceClean.API.Extensions;
using eCommerceClean.API.Responses;
using eCommerceClean.Application.Features.ProductDto.Get;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace eCommerceClean.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController(ISender sender) : ControllerBase
    {
        [HttpGet("notfound")]
        public async Task<IActionResult> GetNotFound()
        {
            var result = await sender.Send(new GetProductQuery(55));
            return result.ToActionResult();
        }

        [HttpGet("servererror")]
        public async Task<IActionResult> GetServerError()
        {
            var result = await sender.Send(new GetProductQuery(55));
            var str = result.ToString();
            str = result.Data.ToString();
            return result.ToActionResult();
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(HttpStatusCode.BadRequest));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            // ex: pass in "two" as id
            return Ok();
        }

        [HttpGet("maths")]
        public ActionResult GetMath()
        {
            int a = 10, b = 0;
            int res = a / b;
            return Ok();
        }
    }
}
