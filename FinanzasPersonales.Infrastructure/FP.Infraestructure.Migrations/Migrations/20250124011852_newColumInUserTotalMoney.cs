using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanciasPersonalesApiRest.Migrations
{
    /// <inheritdoc />
    public partial class newColumInUserTotalMoney : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalMoney",
                table: "User",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalMoney",
                table: "User");
        }
    }
}
