using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayPointAPi.DataAccess.Data;
using PayPointAPI.DTOs;
using PayPointAPI.Models.Models;

namespace PayPointAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _context.Products.ToList();
            if (products == null || !products.Any())
            {
                return NotFound("No products found.");
            }
            return Ok(products);
        }
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }
            return Ok(product);

        }
        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            if (product == null)
            {
                return BadRequest("Car data is null");
            }
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(Product product, int id)
        {
            if (product == null || product.ProductId != id)
            {
                return BadRequest("Product data is null or ID mismatch.");
            }
            var existingProduct = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if (existingProduct == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }
            try
            {
                existingProduct.ProductName = product.ProductName;
                existingProduct.ProductDescription = product.ProductDescription;
                existingProduct.Price = product.Price;
                existingProduct.StockQuantity = product.StockQuantity;
                existingProduct.ExpiryDate = product.ExpiryDate;
                existingProduct.CategoryId = product.CategoryId;
                _context.SaveChanges();
                return Ok(existingProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }
            try
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                return Ok($"Product with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
