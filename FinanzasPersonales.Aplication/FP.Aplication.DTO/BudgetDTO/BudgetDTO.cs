using FinanciasPersonalesApiRest.Models;


namespace FinanciasPersonalesApiRest.DTOs.BudgetDTO
{
    public class BudgetDTO
    {
        public int IdBudget { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal TotalBudget { get; set; }    // monto del presupuesto
        public DateTime Date { get; set; }     // fecha de creacion
        public ICollection<BudgetCategory> BudgetLists { get; set; } = new List<BudgetCategory>();
        public int IdUser { get; set; }

    }
}
