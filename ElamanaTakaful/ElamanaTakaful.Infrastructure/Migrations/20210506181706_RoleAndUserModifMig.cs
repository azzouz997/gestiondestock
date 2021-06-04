using Microsoft.EntityFrameworkCore.Migrations;

namespace ElamanaTakaful.Infrastructure.Migrations
{
    public partial class RoleAndUserModifMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FunctionRole_Functions_FunctionsId",
                table: "FunctionRole");

            migrationBuilder.DropForeignKey(
                name: "FK_FunctionRole_Roles_RolesId",
                table: "FunctionRole");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Suppliers",
                newName: "SupplierId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Roles",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Functions",
                newName: "FunctionId");

            migrationBuilder.RenameColumn(
                name: "RolesId",
                table: "FunctionRole",
                newName: "RolesRoleId");

            migrationBuilder.RenameColumn(
                name: "FunctionsId",
                table: "FunctionRole",
                newName: "FunctionsFunctionId");

            migrationBuilder.RenameIndex(
                name: "IX_FunctionRole_RolesId",
                table: "FunctionRole",
                newName: "IX_FunctionRole_RolesRoleId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_FunctionRole_Functions_FunctionsFunctionId",
                table: "FunctionRole",
                column: "FunctionsFunctionId",
                principalTable: "Functions",
                principalColumn: "FunctionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FunctionRole_Roles_RolesRoleId",
                table: "FunctionRole",
                column: "RolesRoleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FunctionRole_Functions_FunctionsFunctionId",
                table: "FunctionRole");

            migrationBuilder.DropForeignKey(
                name: "FK_FunctionRole_Roles_RolesRoleId",
                table: "FunctionRole");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "SupplierId",
                table: "Suppliers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Roles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Products",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "FunctionId",
                table: "Functions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "RolesRoleId",
                table: "FunctionRole",
                newName: "RolesId");

            migrationBuilder.RenameColumn(
                name: "FunctionsFunctionId",
                table: "FunctionRole",
                newName: "FunctionsId");

            migrationBuilder.RenameIndex(
                name: "IX_FunctionRole_RolesRoleId",
                table: "FunctionRole",
                newName: "IX_FunctionRole_RolesId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FunctionRole_Functions_FunctionsId",
                table: "FunctionRole",
                column: "FunctionsId",
                principalTable: "Functions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FunctionRole_Roles_RolesId",
                table: "FunctionRole",
                column: "RolesId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
