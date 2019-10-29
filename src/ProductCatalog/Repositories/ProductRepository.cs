using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;
using ProductCatalog.ViewModels.ProductViewModels;

namespace ProductCatalog.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreDataContext _context;

        public ProductRepository(StoreDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ListProductViewModel>> GetProducts()
        {
            return await _context.Products
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
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product> FindProduct(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task Save(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Product product)
        {
            _context.Entry<Product>(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}