using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using PayPointAPi.DataAccess.Data;
using PayPointAPI.DTOs;
using PayPointAPI.Models.Models;

namespace PayPointAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly AppDbContext _context;
        public StoreController(AppDbContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public IActionResult GetStoresList()
        {
            var stores = _context.Stores.ToList();
            if (stores == null || !stores.Any())
            {
                return NotFound("No stores found.");
            }
            return Ok(stores);
        }
        [HttpGet("{id}")]
        public IActionResult GetStore(int id)
        {
            var store = _context.Stores.FirstOrDefault(s => s.StoreId == id);
            if (store == null)
            {
                return NotFound($"Store with ID {id} not found.");
            }
            return Ok(store);
        }
        [HttpPost]
        public IActionResult AddStore(Store store)
        {
            if (store == null)
            {
                return BadRequest("Store data is null");
            }
            try
            {
                _context.Stores.Add(store);
                _context.SaveChanges();
                return Ok(store);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateStore(int id, Store updatestore)
        {
            if (updatestore == null || updatestore.StoreId != id)
            {
                return BadRequest("Store data is null or ID mismatch");
            }
            var existingStore = _context.Stores.FirstOrDefault(s => s.StoreId == id);

            if (existingStore == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }
            try
            {
                existingStore.StoreName = updatestore.StoreName;
                existingStore.Location = updatestore.Location;
                _context.Stores.Update(existingStore);
                _context.SaveChanges();
                return Ok(existingStore);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteStore(int id)
        {
            var deleteStore = _context.Stores.FirstOrDefault(s => s.StoreId == id);
            if (deleteStore == null)
            {
                return NotFound($"Store with ID {id} not found.");
            }
            try
            {
                _context.Stores.Remove(deleteStore);
                _context.SaveChanges();
                return Ok($"Store with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("store/{storeId}/products")]
        public async Task<IActionResult> GetProductsByStore(int storeId)
        {
            var products = await _context.Products
                .Where(p => p.StoreId == storeId)
                .Select(p => new ProductDTO
                {
                    ProductName = p.ProductName,
                    ProductDescription = p.ProductDescription,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    ExpiryDate = p.ExpiryDate,
                    CategoryId = p.CategoryId
                })
                .ToListAsync();
            return Ok(products);
        }

    }
}
