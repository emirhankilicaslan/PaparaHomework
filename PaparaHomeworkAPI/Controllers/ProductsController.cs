using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PaparaHomeworkAPI.Entities;
using PaparaHomeworkAPI.Services;

namespace PaparaHomeworkAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetProducts();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Product product)
        {
            await _productService.CreateProduct(product);
            return Ok("Urun basariyla olusturuldu.");
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody]Product product)
        {
            try
            {
                await _productService.UpdateProduct(product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _productService.GetProductById(product.ProductId) == null)
                    return NotFound();
                throw;
            }
            return Ok("Urun basariyla guncellendi.");
        }
        [HttpPatch]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<Product> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var result = await _productService.UpdateProductPartial(id, patchDocument);

            if(!result)
            {
                return NotFound();
            }

            return Ok("Urun basariyla guncellendi.");
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            await _productService.DeleteProduct(id);
            return Ok("Urun silindi.");
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsContainsSearchKeyword([FromQuery]string keyword)
        {
            var product = await _productService.GetProductsContainsSearchKeyword(keyword);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpGet]
        public async Task<IActionResult> GetProductNameByMaxPrice()
        {
            var productName = await _productService.GetProductNameByMaxPrice();
            if (productName == null)
            {
                return NotFound();
            }
            return Ok(productName);
        }
    }
}