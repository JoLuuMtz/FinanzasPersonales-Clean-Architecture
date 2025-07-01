using FinanzasPersonales.Aplication

namespace FinanzasPersonales.Aplication.FP.Aplication.Interfaces.Respository
{
    public interface IBudgetCategoryRepository : IRepository<BudgetCategory>
    {
        public Task<BudgetCategory> GetBudgetCategoryByName(string name);
    }
}
