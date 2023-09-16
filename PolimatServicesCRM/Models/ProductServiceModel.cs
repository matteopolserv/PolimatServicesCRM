using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolimatServicesCRM.Models
{
    public class ProductServiceModel
    {
        [Key]
        public string ProductServiceId { get; set; } = string.Empty;
        public string ProductServiceName { get; set; } = string.Empty;
        public decimal ProductServicePrice { get; set; }
        public int ProductServiceNumber { get; set; }
        public DateTime MadeDate { get; set; } = DateTime.Now; 
        public decimal ProductServiceNetAmmount { get; set; }
        public int ProductServiceVat { get; set;}
        public decimal ProductServiceVatAmmount { get; set ; }        
        public decimal ProductServiceTotal { get; set; }
        public string Notices { get; set; } = string.Empty;
        public bool Payed { get; set; }
        public string ClientId { get; set; } = string.Empty;
        public string InvoiceModelInvoiceId { get; set; } = string.Empty;
        
        public void SetNetBrutAmmounts()
        {
            ProductServiceNetAmmount = ProductServicePrice * ProductServiceNumber;
            ProductServiceVatAmmount = ProductServiceNetAmmount * ProductServiceVat / 100;
            ProductServiceTotal = ProductServiceNetAmmount + ProductServiceVatAmmount;
        }
    }
}
