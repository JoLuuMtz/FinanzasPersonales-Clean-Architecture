



namespace FinanzasPersonales.Aplication
{
    public interface IBudgetService
    {

        public Task<ServiceResult<BudgetDTO>> CreateBudget(CreateBudgetDTO budgetDTO);
        public Task<ServiceResult<BudgetDTO>> UpdateBudget(int id, UpdateBudgetDTO budgetDTO);
        public Task<ServiceResult<BudgetDTO>> DeleteBudget(int id);
        public Task<ServiceResult<BudgetDTO>> GetBudget(int id);
        public Task<bool> getBudgetByName(string name);


    }
}
