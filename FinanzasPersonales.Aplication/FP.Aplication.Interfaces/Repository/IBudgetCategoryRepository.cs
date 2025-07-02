using FinanzasPersonales.Domain;

namespace FinanzasPersonales.Aplication
{
    public interface IBudgetCategoryRepository : IRepository<BudgetCategory>
    {
        public Task<BudgetCategory> GetBudgetCategoryByName(string name);
    }
}
