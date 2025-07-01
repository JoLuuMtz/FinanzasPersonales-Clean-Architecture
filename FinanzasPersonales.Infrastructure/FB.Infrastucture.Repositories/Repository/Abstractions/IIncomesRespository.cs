using FinanciasPersonalesApiRest.Models;

namespace FinanciasPersonalesApiRest.Repository.Interfaces
{
    public interface IIncomesRespository
    {
        Task<IEnumerable<Income>> GetIncomesByUserIdAsync(int userId); // Método para ingresos por usuario
    }
}
