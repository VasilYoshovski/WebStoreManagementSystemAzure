using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class StoreSystemDbContext: DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<ProductOffer> ProductOffers { get; set; }
        public DbSet<ProductSale> ProductSales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
           if(!builder.IsConfigured)
            {
                builder.UseSqlServer("Server=.//SQLEXPRESS;Database=StoreSystem;Trusted_Connection=True;");
                //Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Relations
            modelBuilder.Entity<ProductSale>().HasKey(x => new { x.ProductID, x.SaleID });

            modelBuilder.Entity<ProductSale>().HasOne<Product>(ps => ps.Product).WithMany(p => p.ProductSales);
            modelBuilder.Entity<ProductSale>().HasOne<Sale>(ps => ps.Sale).WithMany(s => s.ProductsInSale);

            modelBuilder.Entity<ProductOffer>().HasOne<Product>(po => po.Product).WithMany(p => p.ProductOffers);
            modelBuilder.Entity<ProductOffer>().HasOne<Offer>(po => po.Offer).WithMany(o => o.ProductsInOffer);

            modelBuilder.Entity<Sale>().HasOne<Offer>(s => s.Offer).WithOne(o => o.Sale).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Offer>().HasOne<Sale>(o => o.Sale).WithOne(s => s.Offer).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Offer>().HasOne<Client>(o => o.Client).WithMany(/*...*/);

            modelBuilder.Entity<Product>().HasOne<Supplier>(p => p.Supplier).WithMany(/*...*/);

            //Constrains


        }
    }
}
