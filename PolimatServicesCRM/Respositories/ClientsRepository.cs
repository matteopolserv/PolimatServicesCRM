using Microsoft.EntityFrameworkCore;
using PolimatServicesCRM.Data;
using PolimatServicesCRM.Interfaces;
using PolimatServicesCRM.Models;

namespace PolimatServicesCRM.Respositories
{
    public class ClientsRepository : IClientsRepository
    {
        private readonly ApplicationDbContext _ctx;
        public ClientsRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<bool> AddNewClient(ClientModel client)
        {
            client.ClientId = Guid.NewGuid().ToString();
            await _ctx.Clients.AddAsync(client);
            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> ClientExists(string id)
        {
            return await _ctx.Clients.FirstOrDefaultAsync(client => client.ClientId.Equals(id)) is not null;
            
        }

        public async Task<bool> DeleteNewClient(ClientModel client)
        {
            _ctx.Clients.Remove(client);
            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<ClientModel>> GetAllClients()
        {
            return await _ctx.Clients.OrderBy(x => x.ClientName).ToListAsync();
        }

        public async Task<ClientModel> GetClientById(string id)
        {
            return await _ctx.Clients.FirstOrDefaultAsync(client => client.ClientId.Equals(id)) ?? new ClientModel();
        }

        public async Task<List<InvoiceModel>> GetClientInvoices(string id)
        {
            return await _ctx.Invoices.Where(inv => inv.ClientId.Equals(id)).ToListAsync();
        }

        public async Task<List<ProductServiceModel>> GetClientProductsServices(string id)
        {
            return await _ctx.ProductsServices.Where(product => product.ClientId.Equals(id)).ToListAsync();
        }

        public async Task<bool> UpdateClient(ClientModel client)
        {
            _ctx.Clients.Update(client);
            return await _ctx.SaveChangesAsync() > 0;
        }
    }
}
