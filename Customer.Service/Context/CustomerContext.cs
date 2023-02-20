using Customer.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Customer.Service.Context
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options) { }
        public DbSet<CustomerData> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerData>().ToTable("Customer");
        }
    }
}
