using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;

namespace PolimatServicesCRM.Models
{
    public class InvoiceModel
    {
        [Key]
        public string InvoiceId { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
        public int InvoiceNumber { get; set; }
        public List<ProductServiceModel> Products { get; set; } = new();
        public DateTime CretedTime { get; set; } = DateTime.Now;
        public DateTime PaymentDeadline { get; set; } = DateTime.Now.AddDays(14);
        public string CreatedBy { get; set; } = "Mateusz Chejzdral";
        public string RceivedBy { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public List<string> PaymentMethods { get;} = new() 
        {
            "Gotówka",
            "Przelew na konto",
            "Karta",
            "Płatność online"
        };
        public bool Payed { get; set; }
       
        public decimal TotalVatAmmount { get; set; }
        
        
        public decimal TotalNetAmmount {get; set; }
        public decimal TotalBrutAmmount { get; set; }
        
        public void SetNetBrutAmmount()
        {
            foreach (var product in  Products)
            {
                product.ProductServiceNetAmmount = product.ProductServiceNumber * product.ProductServicePrice;
                product.ProductServiceVatAmmount = product.ProductServiceVat * product.ProductServicePrice / 100;
                product.ProductServiceTotal = product.ProductServiceNetAmmount + product.ProductServiceVatAmmount;
            }
            TotalVatAmmount = Products.Sum(product => product.ProductServiceVatAmmount);
            TotalNetAmmount = Products.Sum(product => product.ProductServiceNetAmmount);
            TotalBrutAmmount = Products.Sum(product => product.ProductServiceTotal);
        }
    }

    
}
