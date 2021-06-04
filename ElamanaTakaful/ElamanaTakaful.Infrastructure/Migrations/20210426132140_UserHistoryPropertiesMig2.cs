using Microsoft.EntityFrameworkCore.Migrations;

namespace ElamanaTakaful.Infrastructure.Migrations
{
    public partial class UserHistoryPropertiesMig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddUser",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeactivateUser",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ModifyUser",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddUser",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeactivateUser",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModifyUser",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
