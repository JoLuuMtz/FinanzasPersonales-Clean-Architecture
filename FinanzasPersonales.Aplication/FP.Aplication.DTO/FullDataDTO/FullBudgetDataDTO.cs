using FinanzasPersonales.Domain;


namespace FinanzasPersonales.Aplication
{
    public class FullBudgetDataDTO
    {
        public int IdBudget { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime Date { get; set; }
        public ICollection<FullBudgetCategoryDTO> BudgetLists { get; set; } = new List<FullBudgetCategoryDTO>();
    }
}
