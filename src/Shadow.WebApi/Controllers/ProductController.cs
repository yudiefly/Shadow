using Microsoft.AspNetCore.Mvc;
using Shadow.IService.Sample;
using Shadow.Tool.Http.Filters;
using Shadow.WebApi.Models;
using System;
using System.Threading.Tasks;

namespace Shadow.WebApi.Controllers
{
    // SqlServer
    [NoLog]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductInboundServiceSample _productInboundService;

        public ProductController(IProductInboundServiceSample productInboundService)
        {
            _productInboundService = productInboundService;
        }

        [ValidateInputModel]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductCreateViewModel model)
        {
            var products = await _productInboundService.AddProductAsync(model.NO, model.Name, model.Weight);
            return Ok(products);
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var product = await _productInboundService.GetProductsAsync(id);
            return Ok(product);
        }

        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _productInboundService.GetAllProductsAsync();
            return Ok(products);
        }

        [Route("count")]
        [HttpGet]
        public async Task<IActionResult> Count()
        {
            var count = await _productInboundService.GetAllProductCountAsync();
            return Ok(count);
        }

        [Route("del")]
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _productInboundService.RemoveProductAsync(id);
            return Ok();
        }
    }

    // MySQL
    [NoLog]
    [Route("api/[controller]")]
    public class Product2Controller : Controller
    {
        private readonly IProduct2InboundServiceSample _product2InboundService;

        public Product2Controller(IProduct2InboundServiceSample product2InboundService)
        {
            _product2InboundService = product2InboundService;
        }

        [ValidateInputModel]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductCreateViewModel model)
        {
            var products = await _product2InboundService.AddProductAsync(model.NO, model.Name, model.Weight);
            return Ok(products);
        }

        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            var product = await _product2InboundService.GetProductsAsync(id);
            return Ok(product);
        }

        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _product2InboundService.GetAllProductsAsync();
            return Ok(products);
        }

        [Route("count")]
        [HttpGet]
        public async Task<IActionResult> Count()
        {
            var count = await _product2InboundService.GetAllProductCountAsync();
            return Ok(count);
        }

        [Route("del")]
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await _product2InboundService.RemoveProductAsync(id);
            return Ok();
        }
    }
}
