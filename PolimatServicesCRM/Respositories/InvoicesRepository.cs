using Microsoft.EntityFrameworkCore;
using PolimatServicesCRM.Data;
using PolimatServicesCRM.Interfaces;
using PolimatServicesCRM.Models;

namespace PolimatServicesCRM.Respositories
{
    public class InvoicesRepository : IInvoicesRepository
    {
        private readonly ApplicationDbContext _ctx;
        public InvoicesRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<bool> AddInvoice(InvoiceModel invoice)
        {            
            invoice.InvoiceId = Guid.NewGuid().ToString();
            foreach (var prod in invoice.Products)
            {
                prod.InvoiceModelInvoiceId = invoice.InvoiceId;
                prod.ProductServiceId = Guid.NewGuid().ToString();
            }
            invoice.SetNetBrutAmmount();
            await _ctx.Invoices.AddAsync(invoice);
            return _ctx.SaveChanges() > 0;
        }

        public async Task<bool> DeleteInvoice(InvoiceModel invoice)
        {
            _ctx.Invoices.Remove(invoice);
            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<InvoiceModel>> GetAllInvoices()
        {
            var invoices = await _ctx.Invoices.OrderByDescending(x => x.InvoiceNumber).ToListAsync();
            foreach (var invoice in invoices)
            {
                List<ProductServiceModel> products = await _ctx.ProductsServices.Where(prod => prod.InvoiceModelInvoiceId.Equals(invoice.InvoiceId)).ToListAsync();
                invoice.Products = products;
               
            }
            return invoices;
        }

        public async Task<ClientModel> GetClientByInvoiceId(string invoiceId)
        {
            InvoiceModel invoice = new();
            invoice = await _ctx.Invoices.FirstOrDefaultAsync(invoice => invoice.InvoiceId.Equals(invoiceId));
            return _ctx.Clients.FirstOrDefault(client => client.ClientId.Equals(invoice.ClientId)) ?? new ClientModel();
        }

        public async Task<InvoiceModel> GetInvoiceById(string id)
        {
            var invoice = await _ctx.Invoices.FirstOrDefaultAsync(invoice => invoice.InvoiceId.Equals(id)) ?? new InvoiceModel();
            List<ProductServiceModel> products = await _ctx.ProductsServices.Where(prod => prod.InvoiceModelInvoiceId.Equals(invoice.InvoiceId)).ToListAsync();
            invoice.Products = products;
            
            return invoice;
        }

        public async Task<bool> InvoiceExists(string id)
        {
            return await _ctx.Invoices.FirstOrDefaultAsync(invoice => invoice.InvoiceId.Equals(id)) is not null;
        }

        public async Task<bool> UpdateInvoice(InvoiceModel invoice)
        {
            invoice.SetNetBrutAmmount();
            _ctx.Invoices.Update(invoice);
            return await _ctx.SaveChangesAsync() > 0;
        }
    }
}
