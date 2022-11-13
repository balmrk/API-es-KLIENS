using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindApiDemo.Data;
using NorthwindApiDemo.Domain;

namespace NorthwindApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public ProductsController(NorthwindContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(short id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(short id, [FromBody] UpdateProductRequest request)
        {
            //var products = await _context.Products.FindAsync(id);
            var products = _context.Products.SingleOrDefault(c => c.ProductId == id);
            if (products is null)
            {
                return NotFound();
            }

            products.ProductName = request.ProductName;
            products.UnitPrice = request.UnitPrice;

            _context.Update(products);
            _context.SaveChanges();

            return Ok();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromBody] CreateProductRequest product)
        {
            var Product = new Product
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                UnitPrice = product.UnitPrice
            };

            _context.Products.Add(Product);
            _context.SaveChanges();

            return Created(string.Empty, null);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(short id)
        {
            var product = _context.Products.SingleOrDefault(c => c.ProductId == id);

            if(product is null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            _context.SaveChanges();

            return NoContent();
        }

        private bool ProductExists(short id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
