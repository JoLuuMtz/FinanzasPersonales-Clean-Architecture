


namespace FinanzasPersonales.Aplication
{
    public interface IRepository<Entity> where Entity : class
    {
        Task<IEnumerable<Entity>> GetAll();
        Task<Entity> GetById(int id);
        Task Create(Entity entity);
        Task Update(Entity entity);
        Task Delete(Entity entity);
        Task Save();


    }

}
