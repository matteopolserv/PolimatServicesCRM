using PolimatServicesCRM.Models;

namespace PolimatServicesCRM.Pages
{
    public partial class ProductsServices
    {
        List<ProductServiceModel> products = new();
        ProductServiceModel product = new();
        ClientModel client = new();
        List<ClientModel> clients = new();
        bool showDetails = false;
        bool editProduct = false;
        bool addProduct = false;
        bool? succesfullyAdded = null;
        bool? succesfullySaved = null;
        bool? succesfullyDeleted = null;

        protected override async Task OnInitializedAsync()
        {
            products = await _products.GetAllProductServices();
            clients = await _clients.GetAllClients();

            if (_state.AddProduct && string.IsNullOrEmpty(_state.ProductId))
            {
                addProduct = _state.AddProduct;
                product = new();
            }
            base.OnInitialized();
        }

        private void Reset()
        {
            editProduct = false;
            showDetails = false;
            addProduct = false;
            succesfullyAdded = null;
            succesfullySaved = null;
            succesfullyDeleted = null;
        }

        private void ShowDetails(int i)
        {
            Reset();
            showDetails = true;
            product = products[i];
            client = clients.FirstOrDefault(c => c.ClientId.Equals(product.ClientId));

        }

        private void EditProduct(int i)
        {
            Reset();
            product = products[i];
            client = clients.FirstOrDefault(c => c.ClientId.Equals(product.ClientId));
            editProduct = true;
        }

        private void AddProduct()
        {
            Reset();
            product = new();
            addProduct = true;
        }


        private async Task AddProductServiceSubmitHandle()
        {
            bool added = await _products.AddProductService(product);
            succesfullyAdded = added;
            if (added)
            {
                product = new();
                await OnInitializedAsync();
            }
        }

        private async Task EditProductServiceSubmitHandle()
        {
            bool saved = await _products.UpdateProductService(product);
            succesfullyAdded = saved;
            if (saved)
            {
                await OnInitializedAsync();
            }
        }
    }
}
