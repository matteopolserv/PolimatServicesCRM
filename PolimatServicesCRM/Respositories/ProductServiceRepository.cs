using Microsoft.EntityFrameworkCore;
using PolimatServicesCRM.Data;
using PolimatServicesCRM.Interfaces;
using PolimatServicesCRM.Models;

namespace PolimatServicesCRM.Respositories
{
    public class ProductServiceRepository : IProductServiceRepository
    {
        private readonly ApplicationDbContext _ctx;
        public ProductServiceRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<bool> AddProductService(ProductServiceModel productService)
        {
            productService.ProductServiceId = Guid.NewGuid().ToString();
            productService.InvoiceModelInvoiceId = "6f980b9d-b106-4b30-9c04-fad8a96651ed";
            productService.SetNetBrutAmmounts();
            await _ctx.ProductsServices.AddAsync(productService);
            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteProductService(ProductServiceModel productService)
        {
            _ctx.ProductsServices.Remove(productService);
            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<ProductServiceModel>> GetAllProductServices()
        {
            return await _ctx.ProductsServices.OrderByDescending(x => x.MadeDate).ToListAsync();
        }

        public async Task<ClientModel> GetClientByProductServiceId(string id)
        {
            ProductServiceModel productService = await _ctx.ProductsServices.FirstOrDefaultAsync(x => x.ProductServiceId.Equals(id));
            ClientModel client = new();
            if(productService is not null)
            {
                client = await _ctx.Clients.FirstOrDefaultAsync(c => c.ClientId.Equals(productService.ClientId))?? new();
            }
            return client;
        }

        public async Task<ProductServiceModel> GetProductServiceById(string id)
        {
            return await _ctx.ProductsServices.FirstOrDefaultAsync(p => p.ProductServiceId.Equals(id)) ?? new();
        }

        public async Task<bool> ProductServiceExists(string id)
        {
            return _ctx.ProductsServices.Any(p => p.ProductServiceId.Equals(id));
        }

        public async Task<bool> UpdateProductService(ProductServiceModel productService)
        {
            productService.SetNetBrutAmmounts();
            _ctx.ProductsServices.Update(productService);
            return await _ctx.SaveChangesAsync() > 0;
        }
    }
}
