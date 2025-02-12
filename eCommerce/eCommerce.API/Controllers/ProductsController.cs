using eCommerce.API.Dtos;
using eCommerce.Core.Entities;
using eCommerce.Core.Interfaces;
using eCommerce.Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IGenericRepository<Product> productRepo, 
                                    IGenericRepository<ProductBrand> productBrandRepo,
                                    IGenericRepository<ProductType> productTypeRepo) : ControllerBase
    {
        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var spec = new ProductWithTypesAndBrandsSpec();
            var products = await productRepo.ListAsync(spec);

            if (products == null || products.Count == 0) { BadRequest("No products found"); }

            var dtos = products!.Select(product => new ProductToReturnDto
            {
                Id = product!.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                PictureUrl = product.PictureUrl,
                ProductBrand = product.ProductBrand.Name,
                ProductType = product.ProductType.Name
            }).ToList();

            return Ok(dtos);
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var spec = new ProductWithTypesAndBrandsSpec(id);
            var product = await productRepo.GetEntityWithSpec(spec);

            if (product == null) { BadRequest("No product found"); }

            var dto = new ProductToReturnDto
            {
                Id = product!.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                PictureUrl = product.PictureUrl,
                ProductBrand = product.ProductBrand.Name,
                ProductType = product.ProductType.Name
            };
            return Ok(dto);
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
            var brands = await productBrandRepo.ListAllAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult> GetAllTypes()
        {
            var types = await productTypeRepo.ListAllAsync();
            return Ok(types);
        }
    }
}
