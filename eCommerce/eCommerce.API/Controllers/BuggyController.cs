//using eCommerce.API.Errors;
//using Infrastructure.Data;
//using Microsoft.AspNetCore.Mvc;
//using System.ComponentModel;
//using System.Net;

//namespace eCommerce.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class BuggyController(StoreContext context) : ControllerBase
//    {
//        [HttpGet("notfound")]
//        public ActionResult GetNotFound()
//        {
//            var test = context.Products.Find(55);
//            if (test is null) return NotFound(new ApiResponse(HttpStatusCode.NotFound));

//            return Ok();
//        }

//        [HttpGet("servererror")]
//        public ActionResult GetServerError()
//        {
//            var test = context.Products.Find(55);
//            var str = test.ToString();

//            return Ok();
//        }

//        [HttpGet("badrequest")]
//        public ActionResult GetBadRequest()
//        {
//            return BadRequest(new ApiResponse(HttpStatusCode.BadRequest));
//        }

//        [HttpGet("badrequest/{id}")]
//        public ActionResult GetBadRequest(int id)
//        {
//            return Ok();
//        }

//        [HttpGet("maths")]
//        public ActionResult GetMath()
//        {
//            int a = 10, b = 0;
//            int res = a / b;

//            return Ok();
//        }
//    }
//}
