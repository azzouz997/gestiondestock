using Microsoft.EntityFrameworkCore.Migrations;

namespace ElamanaTakaful.Infrastructure.Migrations
{
    public partial class QuoteNameMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QuoteFileName",
                table: "QuoteHistory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuoteName",
                table: "Propositions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuoteName",
                table: "PropositionHistory",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuoteFileName",
                table: "QuoteHistory");

            migrationBuilder.DropColumn(
                name: "QuoteName",
                table: "Propositions");

            migrationBuilder.DropColumn(
                name: "QuoteName",
                table: "PropositionHistory");
        }
    }
}
