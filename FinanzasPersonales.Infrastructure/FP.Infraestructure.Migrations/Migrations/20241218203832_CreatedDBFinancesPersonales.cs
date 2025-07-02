using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanzasPersonales.Infrastructure
{
    /// <inheritdoc />
    public partial class CreatedDBFinancesPersonales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TypeIncomes",
                columns: table => new
                {
                    IdTypeIncomes = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeIncomes", x => x.IdTypeIncomes);
                });

            migrationBuilder.CreateTable(
                name: "TypeSpends",
                columns: table => new
                {
                    IdTypeSpends = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeSpends", x => x.IdTypeSpends);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DNI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileImagen = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DateRegister = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "Budget",
                columns: table => new
                {
                    IdBudget = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TotalBudget = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    IdUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budget", x => x.IdBudget);
                    table.ForeignKey(
                        name: "FK_Budget_User_IdUser",
                        column: x => x.IdUser,
                        principalTable: "User",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Incomes",
                columns: table => new
                {
                    IdIncome = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateIncome = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdTypeIncome = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incomes", x => x.IdIncome);
                    table.ForeignKey(
                        name: "FK_Incomes_TypeIncomes_IdTypeIncome",
                        column: x => x.IdTypeIncome,
                        principalTable: "TypeIncomes",
                        principalColumn: "IdTypeIncomes",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Incomes_User_IdUser",
                        column: x => x.IdUser,
                        principalTable: "User",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Spends",
                columns: table => new
                {
                    IdSpend = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateSpend = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdTypeSpend = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spends", x => x.IdSpend);
                    table.ForeignKey(
                        name: "FK_Spends_TypeSpends_IdTypeSpend",
                        column: x => x.IdTypeSpend,
                        principalTable: "TypeSpends",
                        principalColumn: "IdTypeSpends",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Spends_User_IdUser",
                        column: x => x.IdUser,
                        principalTable: "User",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BudgetCategory",
                columns: table => new
                {
                    IdBudgetCategory = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BudgetIdBudget = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetCategory", x => x.IdBudgetCategory);
                    table.ForeignKey(
                        name: "FK_BudgetCategory_Budget_BudgetIdBudget",
                        column: x => x.BudgetIdBudget,
                        principalTable: "Budget",
                        principalColumn: "IdBudget");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Budget_IdUser",
                table: "Budget",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetCategory_BudgetIdBudget",
                table: "BudgetCategory",
                column: "BudgetIdBudget");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_IdTypeIncome",
                table: "Incomes",
                column: "IdTypeIncome");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_IdUser",
                table: "Incomes",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Spends_IdTypeSpend",
                table: "Spends",
                column: "IdTypeSpend");

            migrationBuilder.CreateIndex(
                name: "IX_Spends_IdUser",
                table: "Spends",
                column: "IdUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BudgetCategory");

            migrationBuilder.DropTable(
                name: "Incomes");

            migrationBuilder.DropTable(
                name: "Spends");

            migrationBuilder.DropTable(
                name: "Budget");

            migrationBuilder.DropTable(
                name: "TypeIncomes");

            migrationBuilder.DropTable(
                name: "TypeSpends");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
