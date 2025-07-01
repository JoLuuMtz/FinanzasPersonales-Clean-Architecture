using FinanciasPersonalesApiRest.Data;
using FinanciasPersonalesApiRest.Models;
using FinanciasPersonalesApiRest.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinanciasPersonalesApiRest.Repository
{
    public class BudgetRepository : IRepository<Budget>, IBudgetRepository
    {
        private readonly FinaciasPersonales _context;

        public BudgetRepository(FinaciasPersonales context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Budget>> GetAll()
        {
            return await _context.Budget.ToListAsync();
        }

        public async Task<Budget> GetById(int id)
        {
            return await _context.Budget.FindAsync(id);
        }

        public async Task Create(Budget entity)
        {
            _context.Budget.Add(entity);
            await Save();
        }

        public async Task Update(Budget entity)
        {
            _context.Budget.Update(entity);
            await Save();
        }

        public async Task Delete(Budget entity)
        {
            _context.Budget.Remove(entity);
            await Save();

        }

        public async Task<Budget> GetBudgetByUser(int userId)
        {
            return await _context.Budget
                         .FirstOrDefaultAsync(x => x.IdUser == userId);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        // optiene la entidad por nombre 
        // para verificar si ya existe en el servicio 
        public async Task<Budget> getByName(string name)
            => await _context.Budget
                .Where(x => x.Name.Equals(name))
                .FirstOrDefaultAsync();



    }


}

