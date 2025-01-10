using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.Concrete;
using Entities.Concrete;
using Entities.Concrete.AddressConcrete;
using Entities.Concrete.Auth;
using Entities.Concrete.Shoppings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace DataAccess.Concrete.EntityFramework
{
    public class ECommerceContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            //optionsBuilder.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));



            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<ResetPasswordCode> ResetPasswordCodes { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Brand> Brands { get; set; }
        #region Address
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<County> Counties { get; set; }
        public DbSet<District> Districts { get; set; }
        #endregion
        #region Shoppings
        public DbSet<Order> Orders { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

        #endregion
        public override int SaveChanges()
        {
            var entries = ChangeTracker
        .Entries()
        .Where(e => e.Entity is BaseEntity && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }
            return base.SaveChanges();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Order>()
            //    .HasOne(o => o.Address)
            //    .WithMany(a => a.Orders)
            //    .HasForeignKey(o => o.AddressId);

            ////modelBuilder.Entity<Order>()
            ////    .HasMany(o => o.BasketItems)
            ////    .WithOne(i => i.Order)
            ////    .HasForeignKey(i => i.OrderId);

            //modelBuilder.Entity<Basket>()
            //    .HasMany(b => b.Items)
            //    .WithOne(i => i.Basket)
            //    .HasForeignKey(i => i.BasketId);

            //modelBuilder.Entity<Product>()
            //    .HasMany(p => p.BasketItems)
            //    .WithOne(i => i.Product)
            //    .HasForeignKey(i => i.ProductId);
        }
    }
}
