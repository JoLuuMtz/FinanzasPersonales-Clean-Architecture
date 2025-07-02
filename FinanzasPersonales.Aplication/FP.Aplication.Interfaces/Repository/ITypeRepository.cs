using FinanzasPersonales.Domain;

namespace FinanzasPersonales.Aplication
{
    public interface ITypeRepository<entity> where entity : class
    {
        Task<IEnumerable<entity>> GetAll();
        Task<entity> getByName(string name);
        Task Create(entity entity);

        //Task<entity> GetById(int id);
    }
}
