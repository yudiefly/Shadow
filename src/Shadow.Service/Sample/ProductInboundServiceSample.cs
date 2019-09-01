using ZZH.AutoMapper.Service;
using Shadow.ContractModel;
using Shadow.Entity.Sample;
using Shadow.IRepository.Sample;
using Shadow.IService.Sample;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shadow.Service.Sample
{
    public class ProductInboundServiceSample : IProductInboundServiceSample
    {
        private readonly IProductRepositorySample _productRepository;

        public ProductInboundServiceSample(IProductRepositorySample productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductModel>> GetAllProductsAsync()
        {
            var prods = new List<string> { "001", "002" };
            var products = await _productRepository.GetAllAsync<Product>(x => prods.Contains(x.NO));
            return products.MapTo<ProductModel>();
        }

        public Task<int> GetAllProductCountAsync()
        {
            return _productRepository.CountAsync<Product>(s => !s.IsDeleted);
        }

        public async Task<ProductModel> GetProductsAsync(Guid id)
        {
            var product = await _productRepository.GetAsync<Product>(id);
            return product.MapTo<ProductModel>();
        }

        public async Task<ProductModel> AddProductAsync(string no, string name, double weight)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                NO = no,
                Name = name,
                Weight = weight,
                InBound = DateTime.Now,
            };

            await _productRepository.InsertAsync(product);

            return product.MapTo<ProductModel>();
        }

        public async Task RemoveProductAsync(Guid id)
        {
            var product = await _productRepository.GetAsync<Product>(id);
            await _productRepository.DeleteAsync(product);
        }
    }
}
