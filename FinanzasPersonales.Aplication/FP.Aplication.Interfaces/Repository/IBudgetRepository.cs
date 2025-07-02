
using FinanzasPersonales.Domain;

namespace FinanzasPersonales.Aplication
{
    public interface IBudgetRepository : IRepository<Budget>
    {

        public Task<Budget> GetBudgetByUser(int userId);
        public Task<Budget> getByName(string name);




    }
}
