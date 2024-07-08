using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using PaparaHomeworkAPI.Context;
using PaparaHomeworkAPI.Entities;

namespace PaparaHomeworkAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly PaparaContext _paparaContext;
        public ProductService(PaparaContext paparaContext)
        {
            _paparaContext = paparaContext;
        }
        public async Task<Product> CreateProduct(Product product)
        {
            _paparaContext.Products.Add(product);
            await _paparaContext.SaveChangesAsync();
            return product;
        }

        public async Task DeleteProduct(int id)
        {
            var product = await _paparaContext.Products.FindAsync(id);
            if(product != null)
            {
                _paparaContext.Products.Remove(product);
                await _paparaContext.SaveChangesAsync();
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _paparaContext.Products.FindAsync(id);      
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _paparaContext.Products.ToListAsync();
        }

        public Task<Product> PatchProduct(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            var item = await _paparaContext.Products.FindAsync(product.ProductId);
            item.ProductId = product.ProductId;
            item.ProductName = product.ProductName;
            item.Description = product.Description;
            item.Price = product.Price;
            await _paparaContext.SaveChangesAsync();
            return item;
        }

        public async Task<bool> UpdateProductPartial(int id, JsonPatchDocument<Product> patchDocument)
        {
            var existingProduct = await GetProductById(id);
            if (existingProduct == null)
                return false;

            var product = new Product()
            {
                ProductId = existingProduct.ProductId,
                ProductName = existingProduct.ProductName,
                Description = existingProduct.Description,
                Price = existingProduct.Price,
            };

            patchDocument.ApplyTo(product);

            existingProduct.ProductId = product.ProductId;
            existingProduct.ProductName = product.ProductName;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;

            await _paparaContext.SaveChangesAsync();
            return true;
        }
        public async Task<List<Product>> GetProductsContainsSearchKeyword(string keyword)
        {
            return await _paparaContext.Products.Where(x => x.ProductName.Contains(keyword)).ToListAsync();
        }

        public async Task<string> GetProductNameByMaxPrice()
        {
            var productName = await _paparaContext.Products.Where(x => x.Price == _paparaContext.Products.Max(y => y.Price)).Select(x => x.ProductName).FirstOrDefaultAsync();
            return productName;
        }
    }
}