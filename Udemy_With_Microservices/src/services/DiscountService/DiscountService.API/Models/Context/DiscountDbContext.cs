using DiscountService.API.Configs;
using DiscountService.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiscountService.API.Models.Context
{
    public class DiscountDbContext : DbContext
    {

        public DbSet<Discount> Discounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configurations.GetConnectionString());
        }

        
    }
}
