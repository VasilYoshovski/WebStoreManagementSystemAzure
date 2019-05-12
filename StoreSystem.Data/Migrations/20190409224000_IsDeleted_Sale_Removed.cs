using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreSystem.Data.Migrations
{
    public partial class IsDeleted_Sale_Removed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Sales");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Sales",
                nullable: false,
                defaultValue: false);
        }
    }
}
