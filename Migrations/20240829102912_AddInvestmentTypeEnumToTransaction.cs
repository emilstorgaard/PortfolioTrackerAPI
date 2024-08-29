using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioTrackerAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddInvestmentTypeEnumToTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvestmentType",
                table: "Transactions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InvestmentType",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
