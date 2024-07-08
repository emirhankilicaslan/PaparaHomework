using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PaparaHomeworkAPI.Entities;

namespace PaparaHomeworkAPI.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProductById(int id);
        Task<Product> CreateProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task DeleteProduct(int id);
        Task<Product> PatchProduct(int id);
        Task<bool> UpdateProductPartial(int id, JsonPatchDocument<Product> patchDocument);
        Task<List<Product>> GetProductsContainsSearchKeyword(string keyword);
        Task<string> GetProductNameByMaxPrice();
    }
}