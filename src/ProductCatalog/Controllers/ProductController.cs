using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;
using ProductCatalog.ViewModels;
using ProductCatalog.ViewModels.ProductViewModels;

namespace ProductCatalog.Controllers
{
    [ApiController]
    [Route("api/v1/products")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<ListProductViewModel>>> GetProducts([FromServices] StoreDataContext _context)
        {
            var products = await _context.Products
                .Include(x => x.Category)
                .Select(x => new ListProductViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    CategoryId = x.CategoryId,
                    Category = x.Category.Title
                })
                .AsNoTracking()
                .ToListAsync();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct([FromServices] StoreDataContext _context, int id)
        {
            var product = await _context.Products.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ResultViewModel>> PostProduct(
            [FromServices] StoreDataContext _context,
            [FromBody] EditorProductViewModel model)
        {
            var product = new Product
            {
                Title = model.Title,
                Description = model.Description,
                CategoryId = model.CategoryId,
                CreateDate = DateTime.Now,
                Image = model.Image,
                LastUpdateDate = DateTime.Now,
                Price = model.Price,
                Quantity = model.Quantity
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            var result = new ResultViewModel
            {
                Success = true,
                Message = "Produto cadastrado com sucesso!",
                Data = product
            };

            return CreatedAtAction(nameof(PostProduct), result);
        }

        [HttpPut]
        public async Task<ActionResult<ResultViewModel>> PutProduct(
            [FromServices] StoreDataContext _context,
            [FromBody] EditorProductViewModel model)
        {
            var product = await _context.Products.FindAsync(model.Id);

            product.Title = model.Title;
            product.Description = model.Description;
            product.CategoryId = model.CategoryId;
            product.Image = model.Image;
            product.LastUpdateDate = DateTime.Now;
            product.Price = model.Price;
            product.Quantity = model.Quantity;

            _context.Entry<Product>(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            var result = new ResultViewModel
            {
                Success = true,
                Message = "Produto alterado com sucesso!",
                Data = product
            };

            return CreatedAtAction(nameof(PutProduct), result);
        }
    }
}