using FinanciasPersonalesApiRest.Models;
using FinanciasPersonalesApiRest.Data;
using Microsoft.EntityFrameworkCore;
using FinanciasPersonalesApiRest.Repository.Interfaces;

namespace FinanciasPersonalesApiRest.Repository
{
    public class IncomesRepository : IRepository<Income>, IIncomesRespository
    {
        private readonly FinaciasPersonales _context;
        public IncomesRepository(FinaciasPersonales context) 
        {
            _context = context; 
        }


        public async Task<IEnumerable<Income>> GetAll()
        {
            return await _context.Incomes.ToListAsync();
        }

        public async Task<Income> GetById(int id)
        {

            return await _context.Incomes.FirstOrDefaultAsync(x => x.IdIncome == id);

        }

        public async Task Create(Income incomes)
        {
            await _context.Incomes.AddAsync(incomes);
            await Save();
        }
        public async Task Update(Income incomes)
        {
            _context.Incomes.Update(incomes);
            await Save();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Income incomes)
        {
             _context.Incomes.Remove(incomes);
            await Save();

        }

        // recibe id del usuario autenticado jwt
        // return: lista de ingresos del usuario
        public async Task<IEnumerable<Income>> GetIncomesByUserIdAsync(int userId)
        {
            return await _context.Incomes
            .Where(income => income.IdUser == userId)
            .ToListAsync(); // retorna la lista de ingresos del usuario autenticado
        }
    }
}
