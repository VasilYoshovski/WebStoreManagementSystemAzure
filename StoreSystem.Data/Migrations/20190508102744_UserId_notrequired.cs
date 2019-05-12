using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreSystem.Data.Migrations
{
    public partial class UserId_notrequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StoreUserId",
                table: "Suppliers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "StoreUserId",
                table: "Employees",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "StoreUserId",
                table: "Clients",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StoreUserId",
                table: "Suppliers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StoreUserId",
                table: "Employees",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StoreUserId",
                table: "Clients",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
