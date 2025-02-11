using eCommerceClean.Application.Features.ProductBrandDto.GetAll;
using eCommerceClean.Application.Features.ProductDto.Create;
using eCommerceClean.Application.Features.ProductDto.Get;
using eCommerceClean.Application.Features.ProductDto.GetAll;
using eCommerceClean.Application.Features.ProductTypeDto.GetAll;
using eCommerceClean.Application.Features.ToDo.Create;
using eCommerceClean.Application.Features.ToDo.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eCommerceClean.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(ISender sender) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await sender.Send(new GetAllProductQuery());
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await sender.Send(new GetProductQuery(id));
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProduct dto)
        {
            var result = await sender.Send(new CreateProductCommand(dto));
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut("{id}")]
        public void Update(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("brands")]
        public async Task<ActionResult> GetAllBrands()
        {
            var result = await sender.Send(new GetAllProductBrandQuery());
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("types")]
        public async Task<ActionResult> GetAllTypes()
        {
            var result = await sender.Send(new GetAllProductTypeQuery());
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
