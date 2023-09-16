using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PolimatServicesCRM.Models;

namespace PolimatServicesCRM.Pages
{
    public partial class Invoices
    {
        [Parameter]
        public EventCallback<InvoiceModel> OnClientSelected { get; set; }
        [Parameter]
        public EventCallback<List<InvoiceModel>> InvoiceAdded { get; set; }
        [Parameter]
        public EventCallback<List<ProductServiceModel>> ProductServiceAdded { get; set; }
        [Parameter]
        public EventCallback<List<ProductServiceModel>> ProductServiceRemoved { get; set; }
        InvoiceModel invoice = new();
        ClientModel client = new();

        List<ClientModel> clients;
        List<InvoiceModel> invoices = new();
        [Parameter]
        public List<ProductServiceModel> Products { get; set; } = new();
        ProductServiceModel product = new();
        bool addInvoice = false;
        bool editInvoice = false;
        bool showInvoice = false;
        bool? succesfullyAdded = null;
        bool? succesfullySaved = null;
        bool? succesfullyDeleted = null;
        bool? succesfullySent = null;
        string receivedBy = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            invoices = await _invoices.GetAllInvoices();
            clients = await _clients.GetAllClients();
            Products = new();
            if (!string.IsNullOrEmpty(_transfer.InvoiceId))
            {
                invoice = await _invoices.GetInvoiceById(_transfer.InvoiceId);
                showInvoice = true;
            }

            base.OnInitialized();
        }
        private void Reset()
        {
            addInvoice = false;
            editInvoice = false;
            showInvoice = false;
            succesfullyAdded = null;
            succesfullySaved = null;
            succesfullyDeleted = null;

        }
        private void ShowInvoiceDetails(int i)
        {
            Reset();
            invoice = invoices[i];
            client = clients.FirstOrDefault(c => c.ClientId.Equals(invoice.ClientId));
            showInvoice = true;
            Products = invoice.Products.Where(p => p.InvoiceModelInvoiceId.Equals(invoice.InvoiceId)).ToList();
        }
        private void AddInvoice()
        {
            Reset();
            invoice = new();
            Products = new();
            addInvoice = true;
        }

        private void EditInvoice(int i)
        {
            Reset();
            invoice = invoices[i];
            Products = invoice.Products.Where(p => p.InvoiceModelInvoiceId.Equals(invoice.InvoiceId)).ToList();
            editInvoice = true;
        }

        private async Task AddInvoiceSubmitHandle()
        {
            int invoiceNumber;
            if (invoices.Count > 0) invoiceNumber = invoices.Max(i => i.InvoiceNumber) + 1;
            else invoiceNumber = 1;
            invoice.Products = Products;
            invoice.InvoiceNumber = invoiceNumber;
            bool added = await _invoices.AddInvoice(invoice);
            succesfullyAdded = added;
            if (added) invoice = new();

            await OnInitializedAsync();
        }
        private async Task SaveChangesSubmitHandle()
        {
            invoice.Products = Products;
            bool updated = await _invoices.UpdateInvoice(invoice);
            succesfullySaved = updated;

            await InvoiceAdded.InvokeAsync(invoices);
            await OnInitializedAsync();
        }
        private async Task DeleteInvoiceSubmitHandle()
        {
            bool deleted = await _invoices.DeleteInvoice(invoice);
            await InvoiceAdded.InvokeAsync(invoices);
            Reset();
            succesfullyDeleted = deleted;
            await OnInitializedAsync();
        }
        private void AddNewProductRow() => Products.Add(new ProductServiceModel());
        private void UpdateProductName(int i, string value) => Products[i].ProductServiceName = value;
        private void UpdateProductPrice(int i, string value) => Products[i].ProductServicePrice = decimal.Parse(value);
        private void UpdateProductNumber(int i, string value) => Products[i].ProductServiceNumber = int.Parse(value);
        private void UpdateProductVat(int i, string value) => Products[i].ProductServiceVat = int.Parse(value);

        private void RemoveProductService(int i) => Products.RemoveAt(i);

        private void SelectedClient(int i)
        {
            client = clients[i];
            receivedBy = client.ClientOthers;
            invoice.RceivedBy = receivedBy;
            product.ClientId = client.ClientId;

        }
        private async Task SendInvoiceToClientByEmail(int i)
        {
            invoice = invoices[i];
            ClientModel clientReceiver = await _clients.GetClientById(invoice.InvoiceId);
            PdfFileModel invoiceFile = await _generatePDF.GenerateInvoicePdf(invoice.InvoiceId);
            bool sent = await _sendEmail.SendInvoiceToClient(invoiceFile, "matteo@lotier.pl");
            succesfullySent = sent;
        }

        private async Task DownloadInvoice(int i)
        {
            invoice = invoices[i];
            PdfFileModel invoiceFile = await _generatePDF.GenerateInvoicePdf(invoice.InvoiceId);
            Stream stream = new MemoryStream(invoiceFile.Data);
            using var streamRef = new DotNetStreamReference(stream);
            await JS.InvokeVoidAsync("downloadFileFromStream", invoiceFile.Name, streamRef);
        }
    }
}
