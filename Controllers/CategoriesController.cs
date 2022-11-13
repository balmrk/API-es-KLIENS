using Microsoft.AspNetCore.Mvc;
using NorthwindApiDemo.Data;
using NorthwindApiDemo.Domain;


namespace NorthwindApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public CategoriesController(NorthwindContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _context.Categories;

            if (categories == null)
            {
                return NotFound();
            }

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpGet("count")]
        public int Count() => _context.Categories.Count();

        [HttpGet("{id}/products")]
        public IEnumerable<Product> GetProductsByCategoryId(int id)
        {
            return _context.Products.Where(p => p.CategoryId == id);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateCategoryRequest request)
        {
            var Category = new Category
            {
                CategoryId = request.CategoryId,
                CategoryName = request.CategoryName,
                Description = request.Description
            };

            _context.Categories.Add(Category);
            _context.SaveChanges();

            return Created(string.Empty, null);
        }

    }

}

