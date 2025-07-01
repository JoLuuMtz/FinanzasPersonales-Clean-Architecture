using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanciasPersonalesApiRest.Migrations
{
    /// <inheritdoc />
    public partial class RelacionBudgetBudgetCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BudgetCategory_Budget_BudgetIdBudget",
                table: "BudgetCategory");

            migrationBuilder.DropIndex(
                name: "IX_BudgetCategory_BudgetIdBudget",
                table: "BudgetCategory");

            migrationBuilder.DropColumn(
                name: "BudgetIdBudget",
                table: "BudgetCategory");

            migrationBuilder.AddColumn<int>(
                name: "IdBudget",
                table: "BudgetCategory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BudgetCategory_IdBudget",
                table: "BudgetCategory",
                column: "IdBudget");

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetCategory_Budget_IdBudget",
                table: "BudgetCategory",
                column: "IdBudget",
                principalTable: "Budget",
                principalColumn: "IdBudget",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BudgetCategory_Budget_IdBudget",
                table: "BudgetCategory");

            migrationBuilder.DropIndex(
                name: "IX_BudgetCategory_IdBudget",
                table: "BudgetCategory");

            migrationBuilder.DropColumn(
                name: "IdBudget",
                table: "BudgetCategory");

            migrationBuilder.AddColumn<int>(
                name: "BudgetIdBudget",
                table: "BudgetCategory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BudgetCategory_BudgetIdBudget",
                table: "BudgetCategory",
                column: "BudgetIdBudget");

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetCategory_Budget_BudgetIdBudget",
                table: "BudgetCategory",
                column: "BudgetIdBudget",
                principalTable: "Budget",
                principalColumn: "IdBudget");
        }
    }
}
