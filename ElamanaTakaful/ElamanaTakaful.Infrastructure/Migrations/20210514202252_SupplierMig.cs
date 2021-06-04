using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElamanaTakaful.Infrastructure.Migrations
{
    public partial class SupplierMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FunctionRole");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Suppliers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastBuyDate",
                table: "Suppliers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "OrdersNumber",
                table: "Suppliers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SupplierCode",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FunctionType",
                table: "Functions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Functions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Functions_RoleId",
                table: "Functions",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Functions_Roles_RoleId",
                table: "Functions",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Functions_Roles_RoleId",
                table: "Functions");

            migrationBuilder.DropIndex(
                name: "IX_Functions_RoleId",
                table: "Functions");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "LastBuyDate",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "OrdersNumber",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "SupplierCode",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "FunctionType",
                table: "Functions");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Functions");

            migrationBuilder.CreateTable(
                name: "FunctionRole",
                columns: table => new
                {
                    FunctionsFunctionId = table.Column<int>(type: "int", nullable: false),
                    RolesRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunctionRole", x => new { x.FunctionsFunctionId, x.RolesRoleId });
                    table.ForeignKey(
                        name: "FK_FunctionRole_Functions_FunctionsFunctionId",
                        column: x => x.FunctionsFunctionId,
                        principalTable: "Functions",
                        principalColumn: "FunctionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FunctionRole_Roles_RolesRoleId",
                        column: x => x.RolesRoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FunctionRole_RolesRoleId",
                table: "FunctionRole",
                column: "RolesRoleId");
        }
    }
}
