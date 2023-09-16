using PolimatServicesCRM.Models;

namespace PolimatServicesCRM.Interfaces
{
    public interface IInvoicesRepository
    {
        Task<List<InvoiceModel>> GetAllInvoices();
        Task<InvoiceModel> GetInvoiceById(string id);
        Task<bool> AddInvoice(InvoiceModel invoice);
        Task<bool> UpdateInvoice(InvoiceModel invoice);
        Task<bool> DeleteInvoice(InvoiceModel invoice);
        Task<bool> InvoiceExists (string id);
        Task<ClientModel> GetClientByInvoiceId (string invoiceId);
    }
}
