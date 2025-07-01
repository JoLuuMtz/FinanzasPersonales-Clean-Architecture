using FinanciasPersonalesApiRest.Data;
using FinanciasPersonalesApiRest.Models;
using FinanciasPersonalesApiRest.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinanciasPersonalesApiRest.Repository
{
    public class TypeIncomesRepository : ITypeRepository<TypeIncomes>
    {
        private readonly FinaciasPersonales _context;
        public TypeIncomesRepository(FinaciasPersonales context)
        {
            _context = context;
        }

        public async Task Create(TypeIncomes entity)
        {
            await _context.TypeIncomes.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TypeIncomes>> GetAll()
            => await _context.TypeIncomes.ToListAsync();

        public async Task<TypeIncomes> getByName(string name)
        {
            return await _context.TypeIncomes
                .FirstOrDefaultAsync(t => t.Name == name);
        }

        public async Task<TypeIncomes> GetById(int id) =>
            await _context.TypeIncomes
            .FindAsync(id);

    }
}
