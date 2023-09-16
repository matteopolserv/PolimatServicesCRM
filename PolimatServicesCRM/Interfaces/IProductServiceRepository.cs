using PolimatServicesCRM.Models;

namespace PolimatServicesCRM.Interfaces
{
    public interface IProductServiceRepository
    {
        Task<List<ProductServiceModel>> GetAllProductServices();
        Task<ProductServiceModel> GetProductServiceById(string id);
        Task<bool> AddProductService(ProductServiceModel productService);
        Task<bool> UpdateProductService(ProductServiceModel productService);
        Task<bool> DeleteProductService(ProductServiceModel productService);
        Task<bool> ProductServiceExists(string id);
        Task<ClientModel> GetClientByProductServiceId(string id);
    }
}
