namespace PolimatServicesCRM.Models
{
    public class InvoiceStateModel
    {
        public string InvoiceId { get; set; } = string.Empty;
        public bool ShowDetails { get; set; }
        public bool EditInvoice { get; set; }
        public bool AddInvoice { get; set; }
        public List<ProductServiceModel> Products { get; set; } = new();
    }
}
