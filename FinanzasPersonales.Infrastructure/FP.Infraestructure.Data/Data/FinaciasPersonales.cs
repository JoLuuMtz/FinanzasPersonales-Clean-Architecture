using FinanciasPersonalesApiRest.Models;
using Microsoft.EntityFrameworkCore;


namespace FinanciasPersonalesApiRest.Data
{
    public class FinaciasPersonales : DbContext
    {
        public FinaciasPersonales(DbContextOptions<FinaciasPersonales> options) : base(options)
        { }

        public DbSet<Spend> Spends { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Budget> Budget { get; set; }
        public DbSet<BudgetCategory> BudgetCategory { get; set; }
        public DbSet<TypeIncomes> TypeIncomes { get; set; }
        public DbSet<TypeSpends> TypeSpends { get; set; }
        public DbSet<User> User { get; set; }

        // configuraciones de la base de datos 
        // para la compatibilidad con ET
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                .Property(m => m.DateRegister)
                .HasDefaultValueSql("GETDATE()"); // Especifica el valor por defecto en SQL Server

            // indica que la tabla budgetCategory tiene un trigger
            modelBuilder.Entity<BudgetCategory>()
                 .ToTable(tb => tb.HasTrigger("trg_UpdateTotalBudget"));

            modelBuilder.Entity<Income>()
                .ToTable(tb => tb.HasTrigger("trg_UpdateTotalMoney_Incomes"));


            modelBuilder.Entity<Spend>()
                .ToTable(tb => tb.HasTrigger("trg_UpdateTotalMoney_Spends"));


            modelBuilder.Entity<User>()
                .HasIndex(u => u.DNI)
                .IsUnique();     // Indice unico


            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Budget>()
               .Property(m => m.Date)
               .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<BudgetCategory>()
               .Property(m => m.Date)
               .HasDefaultValueSql("GETDATE()");


        }

    }
}
