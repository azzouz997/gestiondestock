using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElamanaTakaful.Infrastructure.Migrations
{
    public partial class RolesFunctionsRelationshipMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Functions_Roles_RoleId",
                table: "Functions");

            migrationBuilder.DropIndex(
                name: "IX_Functions_RoleId",
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

            migrationBuilder.CreateTable(
                name: "Propositions",
                columns: table => new
                {
                    PropositionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropositionNumber = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PropositionStatus = table.Column<bool>(type: "bit", nullable: false),
                    ValidationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AmountTTC = table.Column<float>(type: "real", nullable: false),
                    AmountHT = table.Column<float>(type: "real", nullable: false),
                    Direction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<float>(type: "real", nullable: false),
                    Quote = table.Column<byte>(type: "tinyint", nullable: false),
                    ValidatorUserId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    SupplierId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Propositions", x => x.PropositionId);
                    table.ForeignKey(
                        name: "FK_Propositions_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Propositions_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Propositions_Users_ValidatorUserId",
                        column: x => x.ValidatorUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuoteHistory",
                columns: table => new
                {
                    QuoteHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuoteFile = table.Column<byte>(type: "tinyint", nullable: false),
                    UptadeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PropositionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuoteHistory", x => x.QuoteHistoryId);
                    table.ForeignKey(
                        name: "FK_QuoteHistory_Propositions_PropositionId",
                        column: x => x.PropositionId,
                        principalTable: "Propositions",
                        principalColumn: "PropositionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FunctionRole_RolesRoleId",
                table: "FunctionRole",
                column: "RolesRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Propositions_ProductId",
                table: "Propositions",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Propositions_SupplierId",
                table: "Propositions",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Propositions_ValidatorUserId",
                table: "Propositions",
                column: "ValidatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteHistory_PropositionId",
                table: "QuoteHistory",
                column: "PropositionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FunctionRole");

            migrationBuilder.DropTable(
                name: "QuoteHistory");

            migrationBuilder.DropTable(
                name: "Propositions");

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
    }
}
