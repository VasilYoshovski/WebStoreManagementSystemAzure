using StoreSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace StoreSystem.Data.DbContext
{
    public class StoreSystemDbContext: IdentityDbContext<StoreUser>
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<ProductOffer> ProductOffers { get; set; }
        public DbSet<ProductSale> ProductSales { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<ProductPurchase> ProductPurchase { get; set; }
        public DbSet<ProductSupplier> productSupplier { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<StoreUser> StoreUsers { get; set; }


        //private static int count = 0;

        public StoreSystemDbContext()
        {
            //count++;
            //System.Console.WriteLine("New db context No: "+count);
        }

        public StoreSystemDbContext(DbContextOptions<StoreSystemDbContext> options)
            :base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
           if(!builder.IsConfigured)
            {
                throw new ArgumentException("Database builder is not configured!");
                builder.UseSqlServer("Server=.\\SQLEXPRESS;Database=StoreSystem;Trusted_Connection=True;");
               // builder.UseSqlServer("Server=.\\SQLEXPRESS;Database=StoreSystemTest;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Relations
            modelBuilder.Entity<ProductSale>().HasKey(x => new { x.ProductID, x.SaleID });
            modelBuilder.Entity<ProductOffer>().HasKey(x => new { x.ProductID, x.OfferID });
            modelBuilder.Entity<ProductPurchase>().HasKey(x => new { x.ProductID, x.PurchaseID });
            modelBuilder.Entity<ProductSupplier>().HasKey(x => new { x.ProductID, x.SupplierID });

            modelBuilder.Entity<ProductSale>().HasOne<Product>(ps => ps.Product).WithMany(p => p.ProductSales);
            modelBuilder.Entity<ProductSale>().HasOne<Sale>(ps => ps.Sale).WithMany(s => s.ProductsInSale);

            modelBuilder.Entity<ProductOffer>().HasOne<Product>(po => po.Product).WithMany(p => p.ProductOffers);
            modelBuilder.Entity<ProductOffer>().HasOne<Offer>(po => po.Offer).WithMany(o => o.ProductsInOffer);

            modelBuilder.Entity<ProductPurchase>().HasOne<Product>(pp => pp.Product).WithMany(p => p.ProductPurchases);
            modelBuilder.Entity<ProductPurchase>().HasOne<Purchase>(pp => pp.Purchase).WithMany(p => p.ProductsТоPurchase);

            modelBuilder.Entity<ProductSupplier>().HasOne<Product>(ps => ps.Product).WithMany(p => p.ProductSuppliers);
            modelBuilder.Entity<ProductSupplier>().HasOne<Supplier>(ps => ps.Supplier).WithMany(s => s.ProductsOfSupplier);

            modelBuilder.Entity<Sale>()
                .HasOne<Offer>(s => s.Offer)
                .WithOne(o => o.Sale)
                .HasForeignKey<Sale>(s => s.OfferID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Offer>()
                .HasOne<Sale>(o => o.Sale)
                .WithOne(s => s.Offer)
                .HasForeignKey<Offer>(o => o.SaleID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Offer>().HasOne<Client>(o => o.Client).WithMany(c => c.Offers).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Offer>().HasOne<Address>(o => o.DeliveryAddress).WithMany(a => a.Offers);
            modelBuilder.Entity<Offer>().HasOne<City>(o => o.DeliveryCity).WithMany(a => a.Offers);
            modelBuilder.Entity<Offer>().HasOne<Country>(o => o.DeliveryCountry).WithMany(a => a.Offers);

            modelBuilder.Entity<Sale>().HasOne<Client>(s => s.Client).WithMany(c => c.Sales).OnDelete(DeleteBehavior.Restrict); ;
            modelBuilder.Entity<Sale>().HasOne<Address>(o => o.DeliveryAddress).WithMany(a => a.Sales);
            modelBuilder.Entity<Sale>().HasOne<City>(o => o.DeliveryCity).WithMany(a => a.Sales);
            modelBuilder.Entity<Sale>().HasOne<Country>(o => o.DeliveryCountry).WithMany(a => a.Sales);

            modelBuilder.Entity<Purchase>().HasOne<Supplier>(p => p.Supplier).WithMany(s => s.Purchases);
            modelBuilder.Entity<Purchase>().HasOne<Warehouse>(p => p.Warehouse).WithMany(s => s.Purchases);

            modelBuilder.Entity<Warehouse>().HasOne<Address>(w => w.Address).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Warehouse>().HasOne<City>(w => w.City).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Warehouse>().HasOne<Country>(w => w.Country).WithMany().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Client>().HasOne<Address>(o => o.Address).WithMany(a => a.Clients);
            modelBuilder.Entity<Client>().HasOne<City>(o => o.City).WithMany(a => a.Clients);
            modelBuilder.Entity<Client>().HasOne<Country>(o => o.Country).WithMany(a => a.Clients);

            modelBuilder.Entity<Supplier>().HasOne<Address>(o => o.Address).WithMany(a => a.Suppliers);
            modelBuilder.Entity<Supplier>().HasOne<City>(o => o.City).WithMany(a => a.Suppliers);
            modelBuilder.Entity<Supplier>().HasOne<Country>(o => o.Country).WithMany(a => a.Suppliers);

            modelBuilder.Entity<StoreUser>()
                .HasOne<Client>(su => su.Client)
                .WithOne(cl => cl.StoreUser)
                .HasForeignKey<StoreUser>(o => o.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StoreUser>()
                .HasOne<Supplier>(su => su.Supplier)
                .WithOne(cl => cl.StoreUser)
                .HasForeignKey<StoreUser>(o => o.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StoreUser>()
                .HasOne<Employee>(su => su.Employee)
                .WithOne(cl => cl.StoreUser)
                .HasForeignKey<StoreUser>(o => o.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            //Constrains


        }
    }
}
