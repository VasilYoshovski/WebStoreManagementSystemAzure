using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreSystem.Data.Migrations
{
    public partial class Add_DIscount_Remove_Discount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductDiscount",
                table: "ProductSales");

            migrationBuilder.DropColumn(
                name: "ProductDiscount",
                table: "ProductOffers");

            migrationBuilder.AddColumn<decimal>(
                name: "ProductDiscount",
                table: "Sales",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProductDiscount",
                table: "Offers",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductDiscount",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "ProductDiscount",
                table: "Offers");

            migrationBuilder.AddColumn<decimal>(
                name: "ProductDiscount",
                table: "ProductSales",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProductDiscount",
                table: "ProductOffers",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
