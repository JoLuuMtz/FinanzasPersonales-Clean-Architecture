﻿
using FinanzasPersonales.Domain;

namespace FinanzasPersonales.Aplication
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByDNIAsync(string dni);
    }
}
