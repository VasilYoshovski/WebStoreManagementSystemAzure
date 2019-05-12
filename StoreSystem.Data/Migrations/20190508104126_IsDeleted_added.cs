using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreSystem.Data.Migrations
{
    public partial class IsDeleted_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Warehouses_AddressID",
                table: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_Warehouses_CityID",
                table: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_Warehouses_CountryID",
                table: "Warehouses");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Suppliers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Products",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Employees",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Clients",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_AddressID",
                table: "Warehouses",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_CityID",
                table: "Warehouses",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_CountryID",
                table: "Warehouses",
                column: "CountryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Warehouses_AddressID",
                table: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_Warehouses_CityID",
                table: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_Warehouses_CountryID",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Clients");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_AddressID",
                table: "Warehouses",
                column: "AddressID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_CityID",
                table: "Warehouses",
                column: "CityID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_CountryID",
                table: "Warehouses",
                column: "CountryID",
                unique: true);
        }
    }
}
