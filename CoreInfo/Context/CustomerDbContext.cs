using CoreInfo.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreInfo.Context
{
    public class CustomerDbContext:DbContext
    {
        public DbSet<Customer> Customers{ get; set; }
        public DbSet<Employee> Employees{ get; set; }
        public DbSet<Product> Products{ get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=CustomerDb.db;User Id=sa;Password=123WsX.456;Encrypt=False");
        }
    }
}
