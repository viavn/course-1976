using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Models;
using ProductCatalog.Repositories;
using ProductCatalog.ViewModels;
using ProductCatalog.ViewModels.ProductViewModels;

namespace ProductCatalog.Controllers
{
    [ApiController]
    [Route("api/v1/products")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListProductViewModel>>> GetProducts([FromServices] IProductRepository _repository)
        {
            var products = await _repository.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct([FromServices] IProductRepository _repository, int id)
        {
            var product = await _repository.GetProduct(id);
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ResultViewModel>> PostProduct(
            [FromServices] IProductRepository _repository,
            [FromBody] EditorProductViewModel model)
        {
            model.Validate();
            if (model.Invalid)
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possível cadastrar o produto",
                    Data = model.Notifications
                };

            var product = new Product
            {
                Title = model.Title,
                Description = model.Description,
                CategoryId = model.CategoryId,
                CreateDate = DateTime.Now,
                ImageUrl = model.Image,
                LastUpdateDate = DateTime.Now,
                Price = model.Price,
                Quantity = model.Quantity
            };

            await _repository.Save(product);

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
            [FromServices] IProductRepository _repository,
            [FromBody] EditorProductViewModel model)
        {
            var product = await _repository.FindProduct(model.Id);

            if (product == null)
            {
                var resultNotFound = new ResultViewModel
                {
                    Success = false,
                    Message = "Produto não encontrado",
                    Data = null
                };

                return NotFound(resultNotFound);
            }

            product.Title = model.Title;
            product.Description = model.Description;
            product.CategoryId = model.CategoryId;
            product.ImageUrl = model.Image;
            product.LastUpdateDate = DateTime.Now;
            product.Price = model.Price;
            product.Quantity = model.Quantity;

            await _repository.Update(product);

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