using PolimatServicesCRM.Models;

namespace PolimatServicesCRM.Pages
{
    public partial class Clients
    {
        ClientModel client = new();
        List<ClientModel> clients;
        bool editClient = false;
        bool showClient = false;
        bool addClient = false;
        bool? savedSuccesfully = null;
        bool? addesSuccesfully = null;
        List<InvoiceModel> invoices = new();
        List<InvoiceModel> tempInvoices = new();
        List<ProductServiceModel> products = new();
        List<ProductServiceModel> tempProducts = new();


        protected override async Task OnInitializedAsync()
        {
            clients = await _clients.GetAllClients();
            invoices = await _invoices.GetAllInvoices();
            products = await _products.GetAllProductServices();

            if (!string.IsNullOrEmpty(_transfer.ClientId))
            {
                client = await _clients.GetClientById(_transfer.ClientId);
                showClient = true;
            }
            base.OnInitialized();

        }
        private void Reset()
        {
            savedSuccesfully = null;
            addesSuccesfully = null;
            showClient = false;
            editClient = false;
            addClient = false;
        }
        private void EditClientInfo(int i)
        {
            Reset();
            editClient = true;
            client = clients[i];
        }
        private void ShowClientInfo(int i)
        {
            Reset();
            showClient = true;
            client = clients[i];
            tempInvoices = invoices.Where(inv => inv.ClientId.Equals(client.ClientId)).ToList();
            tempProducts = products.Where(p => p.ClientId.Equals(client.ClientId)).ToList();
        }
        private void AddClient()
        {
            Reset();
            addClient = true;
            client = new();
        }

        private async Task HandleSaveUserInfoSubmit()
        {
            bool saved = await _clients.UpdateClient(client);
            savedSuccesfully = saved;
            if (saved)
            {
                await OnInitializedAsync();
            }
        }

        private async Task HandleAddUserSubmit()
        {
            bool added = await _clients.AddNewClient(client);
            addesSuccesfully = added;
            if (added)
            {
                client = new();

                await OnInitializedAsync();
            }
        }
        private void RedirectToInvoice(string invoiceId)
        {
            _transfer.InvoiceId = invoiceId;
            _navigation.NavigateTo("/invoices", true);
        }
        private void RedirectToProductService(string productId)
        {
            _transfer.ProductServiceId = productId;
            _navigation.NavigateTo("/productsservices", true);
        }

    }
}
