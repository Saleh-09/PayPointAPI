using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayPointAPi.DataAccess.Data;
using PayPointAPI.Models.Models;

namespace PayPointAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _context.Categories.ToList();
            if (categories == null || !categories.Any())
            {
                return NotFound("No categories found.");
            }
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }
            return Ok(category);
        }
        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            if (category == null)
            {
                return BadRequest("Category data is null");
            }
            try
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
