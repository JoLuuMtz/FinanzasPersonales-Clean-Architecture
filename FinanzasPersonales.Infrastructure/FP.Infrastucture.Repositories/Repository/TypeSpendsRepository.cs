using FinanciasPersonalesApiRest.Data;
using FinanciasPersonalesApiRest.Models;
using FinanciasPersonalesApiRest.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinanciasPersonalesApiRest.Repository
{
    public class TypeSpendsRepository : ITypeRepository<TypeSpends>
    {
        private readonly FinaciasPersonales _context;
        public TypeSpendsRepository(FinaciasPersonales context)
        {
            _context = context;
        }

        public async Task Create(TypeSpends entity)
        {
            await _context.TypeSpends.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<TypeSpends>> GetAll()
            => await _context.TypeSpends.ToListAsync();


        // retorna true si el nombre ya existe
        public async Task<TypeSpends> getByName(string  name)
         => await _context.TypeSpends.FirstOrDefaultAsync(x => x.Name == name);

        //await _context.TypeSpends.FirstOrDefaultAsync(x => x.Name == name);




    }
}

