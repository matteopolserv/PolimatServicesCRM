using PolimatServicesCRM.Models;

namespace PolimatServicesCRM.Interfaces
{
    public interface IClientsRepository
    {
        Task<List<ClientModel>> GetAllClients();
        Task<ClientModel> GetClientById(string id);
        Task<bool> AddNewClient(ClientModel client);
        Task<bool> UpdateClient(ClientModel client);
        Task<bool> DeleteNewClient(ClientModel client);
        Task<bool> ClientExists(string id);
        Task<List<InvoiceModel>> GetClientInvoices(string id);
        Task<List<ProductServiceModel>> GetClientProductsServices(string id);
    }
}
