using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreSystem.Data.Migrations
{
    public partial class Sale_Delivery_Required_Removed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Address_AddressID",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_City_CityID",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Country_CountryID",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Address_AddressID",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_City_CityID",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Country_CountryID",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSupplier_Products_ProductID",
                table: "ProductSupplier");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSupplier_Suppliers_SupplierID",
                table: "ProductSupplier");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Warehouse_WarehouseID",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Address_AddressID",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_City_CityID",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Country_CountryID",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Address_AddressID",
                table: "Suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_City_CityID",
                table: "Suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Country_CountryID",
                table: "Suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouse_Address_AddressID",
                table: "Warehouse");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouse_City_CityID",
                table: "Warehouse");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouse_Country_CountryID",
                table: "Warehouse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSupplier",
                table: "ProductSupplier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Warehouse",
                table: "Warehouse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Country",
                table: "Country");

            migrationBuilder.DropPrimaryKey(
                name: "PK_City",
                table: "City");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.RenameTable(
                name: "ProductSupplier",
                newName: "productSupplier");

            migrationBuilder.RenameTable(
                name: "Warehouse",
                newName: "Warehouses");

            migrationBuilder.RenameTable(
                name: "Country",
                newName: "Countries");

            migrationBuilder.RenameTable(
                name: "City",
                newName: "Cities");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "Addresses");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSupplier_SupplierID",
                table: "productSupplier",
                newName: "IX_productSupplier_SupplierID");

            migrationBuilder.RenameIndex(
                name: "IX_Warehouse_CountryID",
                table: "Warehouses",
                newName: "IX_Warehouses_CountryID");

            migrationBuilder.RenameIndex(
                name: "IX_Warehouse_CityID",
                table: "Warehouses",
                newName: "IX_Warehouses_CityID");

            migrationBuilder.RenameIndex(
                name: "IX_Warehouse_AddressID",
                table: "Warehouses",
                newName: "IX_Warehouses_AddressID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_productSupplier",
                table: "productSupplier",
                columns: new[] { "ProductID", "SupplierID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Warehouses",
                table: "Warehouses",
                column: "WarehouseID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countries",
                table: "Countries",
                column: "CountryID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cities",
                table: "Cities",
                column: "CityID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "AddressID");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Addresses_AddressID",
                table: "Clients",
                column: "AddressID",
                principalTable: "Addresses",
                principalColumn: "AddressID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Cities_CityID",
                table: "Clients",
                column: "CityID",
                principalTable: "Cities",
                principalColumn: "CityID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Countries_CountryID",
                table: "Clients",
                column: "CountryID",
                principalTable: "Countries",
                principalColumn: "CountryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Addresses_AddressID",
                table: "Offers",
                column: "AddressID",
                principalTable: "Addresses",
                principalColumn: "AddressID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Cities_CityID",
                table: "Offers",
                column: "CityID",
                principalTable: "Cities",
                principalColumn: "CityID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Countries_CountryID",
                table: "Offers",
                column: "CountryID",
                principalTable: "Countries",
                principalColumn: "CountryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productSupplier_Products_ProductID",
                table: "productSupplier",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productSupplier_Suppliers_SupplierID",
                table: "productSupplier",
                column: "SupplierID",
                principalTable: "Suppliers",
                principalColumn: "SupplierID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Warehouses_WarehouseID",
                table: "Purchases",
                column: "WarehouseID",
                principalTable: "Warehouses",
                principalColumn: "WarehouseID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Addresses_AddressID",
                table: "Sales",
                column: "AddressID",
                principalTable: "Addresses",
                principalColumn: "AddressID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Cities_CityID",
                table: "Sales",
                column: "CityID",
                principalTable: "Cities",
                principalColumn: "CityID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Countries_CountryID",
                table: "Sales",
                column: "CountryID",
                principalTable: "Countries",
                principalColumn: "CountryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Addresses_AddressID",
                table: "Suppliers",
                column: "AddressID",
                principalTable: "Addresses",
                principalColumn: "AddressID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Cities_CityID",
                table: "Suppliers",
                column: "CityID",
                principalTable: "Cities",
                principalColumn: "CityID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Countries_CountryID",
                table: "Suppliers",
                column: "CountryID",
                principalTable: "Countries",
                principalColumn: "CountryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_Addresses_AddressID",
                table: "Warehouses",
                column: "AddressID",
                principalTable: "Addresses",
                principalColumn: "AddressID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_Cities_CityID",
                table: "Warehouses",
                column: "CityID",
                principalTable: "Cities",
                principalColumn: "CityID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_Countries_CountryID",
                table: "Warehouses",
                column: "CountryID",
                principalTable: "Countries",
                principalColumn: "CountryID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Addresses_AddressID",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Cities_CityID",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Countries_CountryID",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Addresses_AddressID",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Cities_CityID",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Countries_CountryID",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_productSupplier_Products_ProductID",
                table: "productSupplier");

            migrationBuilder.DropForeignKey(
                name: "FK_productSupplier_Suppliers_SupplierID",
                table: "productSupplier");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Warehouses_WarehouseID",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Addresses_AddressID",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Cities_CityID",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Countries_CountryID",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Addresses_AddressID",
                table: "Suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Cities_CityID",
                table: "Suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Countries_CountryID",
                table: "Suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_Addresses_AddressID",
                table: "Warehouses");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_Cities_CityID",
                table: "Warehouses");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_Countries_CountryID",
                table: "Warehouses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_productSupplier",
                table: "productSupplier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Warehouses",
                table: "Warehouses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countries",
                table: "Countries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cities",
                table: "Cities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.RenameTable(
                name: "productSupplier",
                newName: "ProductSupplier");

            migrationBuilder.RenameTable(
                name: "Warehouses",
                newName: "Warehouse");

            migrationBuilder.RenameTable(
                name: "Countries",
                newName: "Country");

            migrationBuilder.RenameTable(
                name: "Cities",
                newName: "City");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "Address");

            migrationBuilder.RenameIndex(
                name: "IX_productSupplier_SupplierID",
                table: "ProductSupplier",
                newName: "IX_ProductSupplier_SupplierID");

            migrationBuilder.RenameIndex(
                name: "IX_Warehouses_CountryID",
                table: "Warehouse",
                newName: "IX_Warehouse_CountryID");

            migrationBuilder.RenameIndex(
                name: "IX_Warehouses_CityID",
                table: "Warehouse",
                newName: "IX_Warehouse_CityID");

            migrationBuilder.RenameIndex(
                name: "IX_Warehouses_AddressID",
                table: "Warehouse",
                newName: "IX_Warehouse_AddressID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSupplier",
                table: "ProductSupplier",
                columns: new[] { "ProductID", "SupplierID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Warehouse",
                table: "Warehouse",
                column: "WarehouseID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Country",
                table: "Country",
                column: "CountryID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_City",
                table: "City",
                column: "CityID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "AddressID");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Address_AddressID",
                table: "Clients",
                column: "AddressID",
                principalTable: "Address",
                principalColumn: "AddressID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_City_CityID",
                table: "Clients",
                column: "CityID",
                principalTable: "City",
                principalColumn: "CityID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Country_CountryID",
                table: "Clients",
                column: "CountryID",
                principalTable: "Country",
                principalColumn: "CountryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Address_AddressID",
                table: "Offers",
                column: "AddressID",
                principalTable: "Address",
                principalColumn: "AddressID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_City_CityID",
                table: "Offers",
                column: "CityID",
                principalTable: "City",
                principalColumn: "CityID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Country_CountryID",
                table: "Offers",
                column: "CountryID",
                principalTable: "Country",
                principalColumn: "CountryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSupplier_Products_ProductID",
                table: "ProductSupplier",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSupplier_Suppliers_SupplierID",
                table: "ProductSupplier",
                column: "SupplierID",
                principalTable: "Suppliers",
                principalColumn: "SupplierID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Warehouse_WarehouseID",
                table: "Purchases",
                column: "WarehouseID",
                principalTable: "Warehouse",
                principalColumn: "WarehouseID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Address_AddressID",
                table: "Sales",
                column: "AddressID",
                principalTable: "Address",
                principalColumn: "AddressID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_City_CityID",
                table: "Sales",
                column: "CityID",
                principalTable: "City",
                principalColumn: "CityID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Country_CountryID",
                table: "Sales",
                column: "CountryID",
                principalTable: "Country",
                principalColumn: "CountryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Address_AddressID",
                table: "Suppliers",
                column: "AddressID",
                principalTable: "Address",
                principalColumn: "AddressID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_City_CityID",
                table: "Suppliers",
                column: "CityID",
                principalTable: "City",
                principalColumn: "CityID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Country_CountryID",
                table: "Suppliers",
                column: "CountryID",
                principalTable: "Country",
                principalColumn: "CountryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouse_Address_AddressID",
                table: "Warehouse",
                column: "AddressID",
                principalTable: "Address",
                principalColumn: "AddressID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouse_City_CityID",
                table: "Warehouse",
                column: "CityID",
                principalTable: "City",
                principalColumn: "CityID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouse_Country_CountryID",
                table: "Warehouse",
                column: "CountryID",
                principalTable: "Country",
                principalColumn: "CountryID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
