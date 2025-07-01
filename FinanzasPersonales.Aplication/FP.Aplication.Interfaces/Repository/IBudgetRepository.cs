using FinanciasPersonalesApiRest.DTOs.BudgetDTO;
using FinanciasPersonalesApiRest.Models;

namespace FinanciasPersonalesApiRest.Repository.Interfaces
{
    public interface IBudgetRepository : IRepository<Budget>
    {

        public Task<Budget> GetBudgetByUser(int userId);
        public Task<Budget> getByName(string name);




    }
}
