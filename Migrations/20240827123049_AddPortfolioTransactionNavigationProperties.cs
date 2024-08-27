using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioTrackerAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddPortfolioTransactionNavigationProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PortfolioId",
                table: "Transactions",
                column: "PortfolioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Portfolios_PortfolioId",
                table: "Transactions",
                column: "PortfolioId",
                principalTable: "Portfolios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Portfolios_PortfolioId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_PortfolioId",
                table: "Transactions");
        }
    }
}
