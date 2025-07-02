

using Microsoft.EntityFrameworkCore;

using FinanzasPersonales.Aplication;
using FinanzasPersonales.Domain;



namespace FinanzasPersonales.Infrastructure;

public class BudgetCategoryRepository : IRepository<BudgetCategory> , IBudgetCategoryRepository
{
    private readonly FinaciasPersonales _context;

    public BudgetCategoryRepository(FinaciasPersonales context)
    {
        _context = context;
    }

    public async Task Create(BudgetCategory entity)
    {
        await _context.BudgetCategory.AddAsync(entity);
        await Save();
    }

    public async Task Delete(BudgetCategory entity)
    {
        _context.BudgetCategory.Remove(entity);
        await Save();
    }

    public async Task<IEnumerable<BudgetCategory>> GetAll()
     => await _context.BudgetCategory.ToListAsync();

    public async Task<BudgetCategory> GetBudgetCategoryByName(string name)
    {
        return await _context.BudgetCategory
            .FirstOrDefaultAsync(x => x.Name == name);

    }

    public async Task<BudgetCategory> GetById(int id)
    {
        return await _context.BudgetCategory.FindAsync(id);
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }

    public async Task Update(BudgetCategory entity)
    {
        _context.BudgetCategory.Update(entity);
        await Save();
    }
}
