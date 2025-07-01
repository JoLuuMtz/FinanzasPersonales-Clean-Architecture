using FinanciasPersonalesApiRest.Models;


namespace FinanciasPersonalesApiRest.DTOs.FullDataDTO
{
    public class FullBudgetDataDTO
    {
        public int IdBudget { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public ICollection<FullBudgetCategoryDTO> BudgetLists { get; set; } = new List<FullBudgetCategoryDTO>();
    }
}
