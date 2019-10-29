using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;

namespace ProductCatalog.Controllers
{
    [ApiController]
    [Route("api/v1/categories")]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetCategory([FromServices] StoreDataContext _context)
        {
            var categories = await _context.Categories.AsNoTracking().ToListAsync();
            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> GetCategory([FromServices] StoreDataContext _context, int id)
        {
            var category = await _context.Categories.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
            return Ok(category);
        }

        [HttpGet("{id:int}/products")]
        public async Task<ActionResult<List<Product>>> GetProducts([FromServices] StoreDataContext _context, int id)
        {
            var products = await _context.Products.AsNoTracking().Where(x => x.Category.Id == id).ToListAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult> PostCategory(
            [FromServices] StoreDataContext _context,
            [FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostCategory), category);
        }

        [HttpPut]
        public async Task<ActionResult> PutCategory(
            [FromServices] StoreDataContext _context,
            [FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry<Category>(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PutCategory), category);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCategory(
            [FromServices] StoreDataContext _context,
            [FromBody] Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}