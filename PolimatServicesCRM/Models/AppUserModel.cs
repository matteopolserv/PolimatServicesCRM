using Microsoft.AspNetCore.Identity;

namespace PolimatServicesCRM.Models
{
    public class AppUserModel : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        private string fullName;
        public string FullName
        {
            get { return FirstName + " " + LastName; }
            set
            {
                FullName = fullName;
            }
        }
        public string Post { get; set; } = string.Empty;
        public string Notices { get; set; } = string.Empty;
    }
}
