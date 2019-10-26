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
        private readonly StoreDataContext _context;

        public CategoryController(StoreDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Category>> GetCategory()
        {
            return await _context.Categories.AsNoTracking().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Category> GetCategory(int id)
        {
            return await _context.Categories.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        [HttpGet("{id}/products")]
        public async Task<IEnumerable<Product>> GetProducts(int id)
        {
            return await _context.Products.AsNoTracking().Where(x => x.Category.Id == id).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> PostCategory([FromBody]Category category)
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
        public async Task<ActionResult> PutCategory([FromBody]Category category)
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
        public async Task<ActionResult> DeleteCategory([FromBody]Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}