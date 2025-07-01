namespace FinanciasPersonalesApiRest.DTOs.BudgetDTO
{
    public class CreateBudgetCategoryDTO
    {
       
        //public int IdBudgetCategory { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int IdBudget { get; set; }

    }
}
