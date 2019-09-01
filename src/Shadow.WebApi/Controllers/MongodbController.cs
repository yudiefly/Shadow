using Microsoft.AspNetCore.Mvc;
using Shadow.IService.Sample;
using Shadow.Tool.Http.Filters;
using Shadow.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shadow.WebApi.Controllers
{
    [NoLog]
    [Route("api/mongodb/product")]
    public class MongodbController : Controller
    {
        private readonly IMongoProductServiceSample _productService;

        public MongodbController(IMongoProductServiceSample productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet]
        [Route("Repeat")]
        public async Task<IActionResult> Repeat(string prefix, int count)
        {
            if (count <= 0)
            {
                return Ok($"count: {count} must large than zero.");
            }

            await _productService.AddProductsAsync(BuildProducts(prefix, count));
            return Ok($"insert successfully, total: {count} items.");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductCreateViewModel model)
        {
            var product = await _productService.AddProductAsync(model.NO, model.Name, model.Weight);
            return Ok(product);
        }

        [HttpGet]
        [Route("count")]
        public async Task<IActionResult> Count()
        {
            var count = await _productService.GetAllCountAsync();

            return Ok(count);
        }

        IEnumerable<Entity.Sample.MongoProduct> BuildProducts(string prefix, int count)
        {
            for (int i = 1; i <= count; i++)
            {
                yield return new Entity.Sample.MongoProduct
                {
                    NO = $"{prefix}_{i}",
                    Name = $"{prefix}_{i}",
                    Weight = i,
                    InBound = DateTime.Now,
                };
            }
        }
    }
}
