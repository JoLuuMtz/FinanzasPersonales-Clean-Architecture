using FinanciasPersonalesApiRest.Models;

namespace FinanciasPersonalesApiRest.Repository.Interfaces
{
    public interface IBudgetCategoryRepository : IRepository<BudgetCategory>
    {
        public Task<BudgetCategory> GetBudgetCategoryByName(string name);
    }
}
