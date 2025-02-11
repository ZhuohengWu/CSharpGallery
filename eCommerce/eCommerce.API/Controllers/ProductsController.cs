using eCommerce.Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductRepository ProductRepo) : ControllerBase
    {
        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var products = await ProductRepo.GetAllAsync();
            return Ok(products);
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await ProductRepo.GetAsync(id));
        }

        // POST api/<ProductsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("brands")]
        public async Task<ActionResult> GetAllBrands()
        {
            var brands = await ProductRepo.GetBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult> GetAllTypes()
        {
            var types = await ProductRepo.GetTypesAsync();
            return Ok(types);
        }
    }
}
