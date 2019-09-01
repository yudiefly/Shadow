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
    public class Product2InboundServiceSample : IProduct2InboundServiceSample
    {
        private readonly IProduct2RepositorySample _productRepository;

        public Product2InboundServiceSample(IProduct2RepositorySample productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product2Model>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync<Product2>();
            return products.MapTo<Product2Model>();
        }

        public Task<int> GetAllProductCountAsync()
        {
            return _productRepository.CountAsync<Product2>(s => !s.IsDeleted);
        }

        public async Task<Product2Model> GetProductsAsync(string id)
        {
            var product = await _productRepository.GetAsync<Product2>(id);
            return product.MapTo<Product2Model>();
        }

        public async Task<Product2Model> AddProductAsync(string no, string name, double weight)
        {
            var product = new Product2
            {
                Id = Guid.NewGuid().ToString(),
                NO = no,
                Name = name,
                Weight = weight,
                InBound = DateTime.Now,
            };

            await _productRepository.InsertAsync(product);

            return product.MapTo<Product2Model>();
        }

        public async Task RemoveProductAsync(string id)
        {
            var product = await _productRepository.GetAsync<Product2>(id);
            await _productRepository.DeleteAsync(product);
        }
    }
}
