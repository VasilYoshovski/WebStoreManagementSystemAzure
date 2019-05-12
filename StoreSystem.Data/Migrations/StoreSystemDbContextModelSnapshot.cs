﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StoreSystem.Data.DbContext;

namespace StoreSystem.Data.Migrations
{
    [DbContext(typeof(StoreSystemDbContext))]
    partial class StoreSystemDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

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

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("StoreUserId");

                    b.Property<string>("UIN")
                        .IsRequired();

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

            modelBuilder.Entity("StoreSystem.Data.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("JobPosition");

                    b.Property<string>("StoreUserId");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");
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

                    b.Property<bool>("IsDeleted");

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

            modelBuilder.Entity("StoreSystem.Data.Models.StoreUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<int?>("ClientId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<int?>("EmployeeId");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<int?>("SupplierId");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("ClientId")
                        .IsUnique()
                        .HasFilter("[ClientId] IS NOT NULL");

                    b.HasIndex("EmployeeId")
                        .IsUnique()
                        .HasFilter("[EmployeeId] IS NOT NULL");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("SupplierId")
                        .IsUnique()
                        .HasFilter("[SupplierId] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("StoreSystem.Data.Models.Supplier", b =>
                {
                    b.Property<int>("SupplierID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressID");

                    b.Property<int>("CityID");

                    b.Property<int>("CountryID");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("StoreUserId");

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

                    b.HasIndex("AddressID");

                    b.HasIndex("CityID");

                    b.HasIndex("CountryID");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("StoreSystem.Data.Models.StoreUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("StoreSystem.Data.Models.StoreUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StoreSystem.Data.Models.StoreUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("StoreSystem.Data.Models.StoreUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
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

            modelBuilder.Entity("StoreSystem.Data.Models.StoreUser", b =>
                {
                    b.HasOne("StoreSystem.Data.Models.Client", "Client")
                        .WithOne("StoreUser")
                        .HasForeignKey("StoreSystem.Data.Models.StoreUser", "ClientId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("StoreSystem.Data.Models.Employee", "Employee")
                        .WithOne("StoreUser")
                        .HasForeignKey("StoreSystem.Data.Models.StoreUser", "EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("StoreSystem.Data.Models.Supplier", "Supplier")
                        .WithOne("StoreUser")
                        .HasForeignKey("StoreSystem.Data.Models.StoreUser", "SupplierId")
                        .OnDelete(DeleteBehavior.Restrict);
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
                        .WithMany()
                        .HasForeignKey("AddressID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("StoreSystem.Data.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("StoreSystem.Data.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
