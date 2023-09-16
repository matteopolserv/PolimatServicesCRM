using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PolimatServicesCRM.Models;

namespace PolimatServicesCRM.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<InvoiceModel> Invoices { get; set; }
        public DbSet<ClientModel> Clients { get; set; }
        public DbSet<ProductServiceModel> ProductsServices { get; set; }
        public DbSet<DelegationModel> Delegations { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}