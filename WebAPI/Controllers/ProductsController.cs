using Application.Services;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Shared.DTOs;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductDto>> Get()
        {
            _logger.LogInformation("Getting all products");
            return await _productService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            _logger.LogInformation("Getting product with id {id}", id);
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                _logger.LogWarning("Product with id {id} not found", id);
                return NotFound();
            }
            return product;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateProductDto createProductDto)
        {
            _logger.LogInformation("Creating a new product");
            await _productService.AddAsync(createProductDto);
            return CreatedAtAction(nameof(Get), new { id = createProductDto.Name }, createProductDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CreateProductDto updateProductDto)
        {
            _logger.LogInformation("Updating product with id {id}", id);
            await _productService.UpdateAsync(id, updateProductDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting product with id {id}", id);
            await _productService.DeleteAsync(id);
            return NoContent();
        }
    }
}