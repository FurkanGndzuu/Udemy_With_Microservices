using Microsoft.EntityFrameworkCore;
using OrderService.Domain.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Context
{
    public class OrderDbContext : DbContext
    {
        public const string Default_Schema = "Ordering";

        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
            
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrdersItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().ToTable("Orders",Default_Schema);
            modelBuilder.Entity<OrderItem>().ToTable("OrderItems", Default_Schema)
                .Property(x => x.ProductPrice).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Order>().OwnsOne(x => x.Address).WithOwner();

            base.OnModelCreating(modelBuilder);

            


        }
    }
}
