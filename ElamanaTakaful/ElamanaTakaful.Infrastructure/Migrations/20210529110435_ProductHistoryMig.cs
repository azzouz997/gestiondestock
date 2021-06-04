using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElamanaTakaful.Infrastructure.Migrations
{
    public partial class ProductHistoryMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Propositions_Users_ValidatorUserId",
                table: "Propositions");

            migrationBuilder.DropIndex(
                name: "IX_Propositions_ValidatorUserId",
                table: "Propositions");

            migrationBuilder.DropColumn(
                name: "Quote",
                table: "Propositions");

            migrationBuilder.DropColumn(
                name: "ValidatorUserId",
                table: "Propositions");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Propositions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuoteId",
                table: "Propositions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ValidatorId",
                table: "Propositions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProductHistory",
                columns: table => new
                {
                    ProductHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuantityInStock = table.Column<float>(type: "real", nullable: false),
                    QuantityUsed = table.Column<float>(type: "real", nullable: false),
                    LastBuyingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductHistory", x => x.ProductHistoryId);
                    table.ForeignKey(
                        name: "FK_ProductHistory_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropositionHistory",
                columns: table => new
                {
                    PropositionHistoryId = table.Column<int>(type: "int", nullable: false)
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
                    QuoteId = table.Column<int>(type: "int", nullable: false),
                    ValidatorId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    PropositionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropositionHistory", x => x.PropositionHistoryId);
                    table.ForeignKey(
                        name: "FK_PropositionHistory_Propositions_PropositionId",
                        column: x => x.PropositionId,
                        principalTable: "Propositions",
                        principalColumn: "PropositionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Propositions_ValidatorId",
                table: "Propositions",
                column: "ValidatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductHistory_ProductId",
                table: "ProductHistory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PropositionHistory_PropositionId",
                table: "PropositionHistory",
                column: "PropositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Propositions_Users_ValidatorId",
                table: "Propositions",
                column: "ValidatorId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Propositions_Users_ValidatorId",
                table: "Propositions");

            migrationBuilder.DropTable(
                name: "ProductHistory");

            migrationBuilder.DropTable(
                name: "PropositionHistory");

            migrationBuilder.DropIndex(
                name: "IX_Propositions_ValidatorId",
                table: "Propositions");

            migrationBuilder.DropColumn(
                name: "QuoteId",
                table: "Propositions");

            migrationBuilder.DropColumn(
                name: "ValidatorId",
                table: "Propositions");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Propositions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<byte>(
                name: "Quote",
                table: "Propositions",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<int>(
                name: "ValidatorUserId",
                table: "Propositions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Propositions_ValidatorUserId",
                table: "Propositions",
                column: "ValidatorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Propositions_Users_ValidatorUserId",
                table: "Propositions",
                column: "ValidatorUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
