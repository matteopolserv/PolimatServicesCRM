using System.ComponentModel.DataAnnotations;

namespace PolimatServicesCRM.Models
{
    public class ClientModel
    {
        [Key]
        public string ClientId { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public string ClientAddress { get; set; } = string.Empty;
        public string ClientCity { get; set; } = string.Empty;  
        public string ClientZip { get; set; } = string.Empty;
        public string ClientCountry { get; set; } = string.Empty;
        [EmailAddress]
        public string ClientEmail { get; set; } = string.Empty;
        public string ClientPhoneNumber { get;  set; } = string.Empty;
        public string Notices { get; set; } = string.Empty;
        public string ClientNip { get; set; } = string.Empty;
        public string ClientRegon { get; set; } = string.Empty;
        public string ClientOthers { get; set; } = string.Empty;

    }
}
