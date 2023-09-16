namespace PolimatServicesCRM.Models
{
    public class InvoiceFileModel
    {
        public string Name { get; set; }    
        public string Extension { get; set; }
        public byte[] Data { get; set; }
    }
}
