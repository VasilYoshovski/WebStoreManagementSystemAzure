﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StoreSystem.Data.DbContext;

namespace StoreSystem.Data.Migrations
{
    [DbContext(typeof(StoreSystemDbContext))]
    [Migration("20190408182225_removed_required_atr")]
    partial class removed_required_atr
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StoreSystem.Data.Models.Address", b =>
                {
                    b.Property<int>("AddressID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("AddressID");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("StoreSystem.Data.Models.City", b =>
                {
                    b.Property<int>("CityID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("CityID");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("StoreSystem.Data.Models.Client", b =>
                {
                    b.Property<int>("ClientID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressID");

                    b.Property<int>("CityID");

                    b.Property<int>("CountryID");

                    b.Property<string>("EIK")
                        .IsRequired();

                    b.Property<string>("Email");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Phone");

                    b.HasKey("ClientID");

                    b.HasIndex("AddressID");

                    b.HasIndex("CityID");

                    b.HasIndex("CountryID");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("StoreSystem.Data.Models.Country", b =>
                {
                    b.Property<int>("CountryID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("CountryID");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("StoreSystem.Data.Models.Offer", b =>
                {
                    b.Property<int>("OfferID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressID");

                    b.Property<int>("CityID");

                    b.Property<int>("ClientID");

                    b.Property<int>("CountryID");

                    b.Property<DateTime>("DeliveryDate");

                    b.Property<DateTime>("ExpiredDate");

                    b.Property<DateTime>("OfferDate");

                    b.Property<decimal>("ProductDiscount");

                    b.Property<int?>("SaleID");

                    b.HasKey("OfferID");

                    b.HasIndex("AddressID");

                    b.HasIndex("CityID");

                    b.HasIndex("ClientID");

                    b.HasIndex("CountryID");

                    b.HasIndex("SaleID")
                        .IsUnique()
                        .HasFilter("[SaleID] IS NOT NULL");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("StoreSystem.Data.Models.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("BuyPrice");

                    b.Property<string>("Measure")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<decimal>("Quantity");

                    b.Property<decimal>("RetailPrice");

                    b.HasKey("ProductID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("StoreSystem.Data.Models.ProductOffer", b =>
                {
                    b.Property<int>("ProductID");

                    b.Property<int>("OfferID");

                    b.Property<decimal>("Quantity");

                    b.HasKey("ProductID", "OfferID");

                    b.HasIndex("OfferID");

                    b.ToTable("ProductOffers");
                });

            modelBuilder.Entity("StoreSystem.Data.Models.ProductPurchase", b =>
                {
                    b.Property<int>("ProductID");

                    b.Property<int>("PurchaseID");

                    b.Property<decimal>("ProductPrice");

                    b.Property<decimal>("ProductQty");

                    b.HasKey("ProductID", "PurchaseID");

                    b.HasIndex("PurchaseID");

                    b.ToTable("ProductPurchase");
                });

            modelBuilder.Entity("StoreSystem.Data.Models.ProductSale", b =>
                {
                    b.Property<int>("ProductID");

                    b.Property<int>("SaleID");

                    b.Property<decimal>("Quantity");

                    b.HasKey("ProductID", "SaleID");

                    b.HasIndex("SaleID");

                    b.ToTable("ProductSales");
                });

            modelBuilder.Entity("StoreSystem.Data.Models.ProductSupplier", b =>
                {
                    b.Property<int>("ProductID");

                    b.Property<int>("SupplierID");

                    b.HasKey("ProductID", "SupplierID");

                    b.HasIndex("SupplierID");

                    b.ToTable("productSupplier");
                });

            modelBuilder.Entity("StoreSystem.Data.Models.Purchase", b =>
                {
                    b.Property<int>("PurchaseID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DeadlineDate");

                    b.Property<DateTime>("DeliveryDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("PurchaseDate");

                    b.Property<int>("SupplierID");

                    b.Property<int>("WarehouseID");

                    b.HasKey("PurchaseID");

                    b.HasIndex("SupplierID");

                    b.HasIndex("WarehouseID");

                    b.ToTable("Purchases");
                });

            modelBuilder.Entity("StoreSystem.Data.Models.Sale", b =>
                {
                    b.Property<int>("SaleID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressID");

                    b.Property<int>("CityID");

                    b.Property<int>("ClientID");

                    b.Property<int>("CountryID");

                    b.Property<DateTime>("DeadlineDate");

                    b.Property<DateTime>("DeliveryDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<int?>("OfferID");

                    b.Property<DateTime>("OrderDate");

                    b.Property<decimal>("ProductDiscount");

                    b.HasKey("SaleID");

                    b.HasIndex("AddressID");

                    b.HasIndex("CityID");

                    b.HasIndex("ClientID");

                    b.HasIndex("CountryID");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("StoreSystem.Data.Models.Supplier", b =>
                {
                    b.Property<int>("SupplierID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressID");

                    b.Property<int>("CityID");

                    b.Property<int>("CountryID");

                    b.Property<string>("Email");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Phone");

                    b.Property<string>("UIN")
                        .IsRequired()
                        .HasMaxLength(9);

                    b.HasKey("SupplierID");

                    b.HasIndex("AddressID");

                    b.HasIndex("CityID");

                    b.HasIndex("CountryID");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("StoreSystem.Data.Models.Warehouse", b =>
                {
                    b.Property<int>("WarehouseID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressID");

                    b.Property<int>("CityID");

                    b.Property<int>("CountryID");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("WarehouseID");

                    b.HasIndex("AddressID")
                        .IsUnique();

                    b.HasIndex("CityID")
                        .IsUnique();

                    b.HasIndex("CountryID")
                        .IsUnique();

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("StoreSystem.Data.Models.Client", b =>
                {
                    b.HasOne("StoreSystem.Data.Models.Address", "Address")
                        .WithMany("Clients")
                        .HasForeignKey("AddressID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StoreSystem.Data.Models.City", "City")
                        .WithMany("Clients")
                        .HasForeignKey("CityID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StoreSystem.Data.Models.Country", "Country")
                        .WithMany("Clients")
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StoreSystem.Data.Models.Offer", b =>
                {
                    b.HasOne("StoreSystem.Data.Models.Address", "DeliveryAddress")
                        .WithMany("Offers")
                        .HasForeignKey("AddressID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StoreSystem.Data.Models.City", "DeliveryCity")
                        .WithMany("Offers")
                        .HasForeignKey("CityID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StoreSystem.Data.Models.Client", "Client")
                        .WithMany("Offers")
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("StoreSystem.Data.Models.Country", "DeliveryCountry")
                        .WithMany("Offers")
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StoreSystem.Data.Models.Sale", "Sale")
                        .WithOne("Offer")
                        .HasForeignKey("StoreSystem.Data.Models.Offer", "SaleID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("StoreSystem.Data.Models.ProductOffer", b =>
                {
                    b.HasOne("StoreSystem.Data.Models.Offer", "Offer")
                        .WithMany("ProductsInOffer")
                        .HasForeignKey("OfferID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StoreSystem.Data.Models.Product", "Product")
                        .WithMany("ProductOffers")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StoreSystem.Data.Models.ProductPurchase", b =>
                {
                    b.HasOne("StoreSystem.Data.Models.Product", "Product")
                        .WithMany("ProductPurchases")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StoreSystem.Data.Models.Purchase", "Purchase")
                        .WithMany("ProductsТоPurchase")
                        .HasForeignKey("PurchaseID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StoreSystem.Data.Models.ProductSale", b =>
                {
                    b.HasOne("StoreSystem.Data.Models.Product", "Product")
                        .WithMany("ProductSales")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StoreSystem.Data.Models.Sale", "Sale")
                        .WithMany("ProductsInSale")
                        .HasForeignKey("SaleID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StoreSystem.Data.Models.ProductSupplier", b =>
                {
                    b.HasOne("StoreSystem.Data.Models.Product", "Product")
                        .WithMany("ProductSuppliers")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StoreSystem.Data.Models.Supplier", "Supplier")
                        .WithMany("ProductsOfSupplier")
                        .HasForeignKey("SupplierID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StoreSystem.Data.Models.Purchase", b =>
                {
                    b.HasOne("StoreSystem.Data.Models.Supplier", "Supplier")
                        .WithMany("Purchases")
                        .HasForeignKey("SupplierID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StoreSystem.Data.Models.Warehouse", "Warehouse")
                        .WithMany("Purchases")
                        .HasForeignKey("WarehouseID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StoreSystem.Data.Models.Sale", b =>
                {
                    b.HasOne("StoreSystem.Data.Models.Address", "DeliveryAddress")
                        .WithMany("Sales")
                        .HasForeignKey("AddressID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StoreSystem.Data.Models.City", "DeliveryCity")
                        .WithMany("Sales")
                        .HasForeignKey("CityID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StoreSystem.Data.Models.Client", "Client")
                        .WithMany("Sales")
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("StoreSystem.Data.Models.Country", "DeliveryCountry")
                        .WithMany("Sales")
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StoreSystem.Data.Models.Supplier", b =>
                {
                    b.HasOne("StoreSystem.Data.Models.Address", "Address")
                        .WithMany("Suppliers")
                        .HasForeignKey("AddressID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StoreSystem.Data.Models.City", "City")
                        .WithMany("Suppliers")
                        .HasForeignKey("CityID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StoreSystem.Data.Models.Country", "Country")
                        .WithMany("Suppliers")
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StoreSystem.Data.Models.Warehouse", b =>
                {
                    b.HasOne("StoreSystem.Data.Models.Address", "Address")
                        .WithOne()
                        .HasForeignKey("StoreSystem.Data.Models.Warehouse", "AddressID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("StoreSystem.Data.Models.City", "City")
                        .WithOne()
                        .HasForeignKey("StoreSystem.Data.Models.Warehouse", "CityID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("StoreSystem.Data.Models.Country", "Country")
                        .WithOne()
                        .HasForeignKey("StoreSystem.Data.Models.Warehouse", "CountryID")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
