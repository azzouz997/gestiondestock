using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElamanaTakaful.Infrastructure.Migrations
{
    public partial class OrderMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<int>(type: "int", nullable: false),
                    CreationStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValidationStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValidationEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorId = table.Column<int>(type: "int", nullable: true),
                    CreatorrId = table.Column<int>(type: "int", nullable: true),
                    ValidatorId = table.Column<int>(type: "int", nullable: true),
                    PropositionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Propositions_PropositionId",
                        column: x => x.PropositionId,
                        principalTable: "Propositions",
                        principalColumn: "PropositionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_CreatorrId",
                        column: x => x.CreatorrId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Users_ValidatorId",
                        column: x => x.ValidatorId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CreatorrId",
                table: "Orders",
                column: "CreatorrId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PropositionId",
                table: "Orders",
                column: "PropositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ValidatorId",
                table: "Orders",
                column: "ValidatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
