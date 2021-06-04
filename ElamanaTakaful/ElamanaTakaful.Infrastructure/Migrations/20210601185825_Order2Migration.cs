using Microsoft.EntityFrameworkCore.Migrations;

namespace ElamanaTakaful.Infrastructure.Migrations
{
    public partial class Order2Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_CreatorrId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CreatorrId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CreatorrId",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CreatorId",
                table: "Orders",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_CreatorId",
                table: "Orders",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_CreatorId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CreatorId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "CreatorrId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CreatorrId",
                table: "Orders",
                column: "CreatorrId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_CreatorrId",
                table: "Orders",
                column: "CreatorrId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
