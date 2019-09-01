using Shadow.ContractModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shadow.IService.Sample
{
    public interface IProductInboundServiceSample
    {
        Task<List<ProductModel>> GetAllProductsAsync();

        Task<int> GetAllProductCountAsync();

        Task<ProductModel> GetProductsAsync(Guid id);

        Task<ProductModel> AddProductAsync(string no, string name, double weight);

        Task RemoveProductAsync(Guid id);
    }
}
