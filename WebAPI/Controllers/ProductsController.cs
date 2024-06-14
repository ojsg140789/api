using Application.Services;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public Task<IEnumerable<Product>> Get()
        {
            return _productService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public Task<Product> Get(int id)
        {
            return _productService.GetByIdAsync(id);
        }

        [HttpPost]
        public Task Post([FromBody] Product product)
        {
            return _productService.AddAsync(product);
        }

        [HttpPut("{id}")]
        public Task Put(int id, [FromBody] Product product)
        {
            product.Id = id;
            return _productService.UpdateAsync(product);
        }

        [HttpDelete("{id}")]
        public Task Delete(int id)
        {
            return _productService.DeleteAsync(id);
        }
    }
}
