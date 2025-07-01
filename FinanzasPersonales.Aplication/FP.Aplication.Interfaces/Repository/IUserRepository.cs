using FinanciasPersonalesApiRest.Models;

namespace FinanciasPersonalesApiRest.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByDNIAsync(string dni);
    }
}
