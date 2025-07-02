using FinanzasPersonales.Domain;

namespace FinanzasPersonales.Aplication
{
    public interface IIncomesRespository
    {
        Task<IEnumerable<Income>> GetIncomesByUserIdAsync(int userId); // Método para ingresos por usuario
    }
}
