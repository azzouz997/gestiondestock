using Microsoft.EntityFrameworkCore.Migrations;

namespace ElamanaTakaful.Infrastructure.Migrations
{
    public partial class routePropMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "status",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "route",
                table: "Functions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "route",
                table: "Functions");
        }
    }
}
