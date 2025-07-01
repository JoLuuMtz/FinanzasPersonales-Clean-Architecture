using FinanciasPersonalesApiRest.Data;
using FinanciasPersonalesApiRest.Models;
using FinanciasPersonalesApiRest.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinanciasPersonalesApiRest.Repository
{
    public class SpendsRepository : IRepository<Spend>
    {
        private readonly FinaciasPersonales _context;
        public SpendsRepository(FinaciasPersonales context) { _context = context; }


        public async Task Create(Spend entity)
        {
            await _context.Spends.AddAsync(entity);
            await Save();
        }

        public async Task Delete(Spend entity)
        {
            _context.Spends.Remove(entity);
            await Save();
        }

        public async Task<IEnumerable<Spend>> GetAll()
        {
            return await _context.Spends.ToListAsync();
        }

        public async Task<Spend> GetById(int id)
        {
           return await _context.Spends.FindAsync(id);
            

        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(Spend entity)
        {
            _context.Spends.Update(entity);
            await Save();
        }


    }
}
