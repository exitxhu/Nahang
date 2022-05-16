using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Nahang.Data.Models;

namespace Nahang.Data
{
    public class DataContext : IdentityDbContext<User, Role, int>
    {
        public DataContext(DbContextOptions<DataContext> op) : base(op)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Cart.CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Order.OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        protected override void OnModelCreating(ModelBuilder mb)
        {
            var user = mb.Entity<User>();
            user.HasKey(n => n.Id);
            base.OnModelCreating(mb);
    }
    }
}
