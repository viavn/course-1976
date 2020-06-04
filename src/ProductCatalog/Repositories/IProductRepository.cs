using System.Collections.Generic;
using System.Threading.Tasks;
using ProductCatalog.Models;
using ProductCatalog.ViewModels.ProductViewModels;

namespace ProductCatalog.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<ListProductViewModel>> GetProducts();
        Task<Product> GetProduct(int id);
        Task<Product> FindProduct(int id);
        Task Save(Product product);
        Task Update(Product product);
    }
}