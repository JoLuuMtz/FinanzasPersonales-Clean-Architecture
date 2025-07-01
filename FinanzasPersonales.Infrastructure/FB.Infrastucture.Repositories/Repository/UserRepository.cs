using FinanciasPersonalesApiRest.Data;
using FinanciasPersonalesApiRest.DTOs.UserDTO;
using FinanciasPersonalesApiRest.Models;
using FinanciasPersonalesApiRest.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FinanciasPersonalesApiRest.Repository
{
    public class UserRepository : IRepository<User>, IUserRepository
    {
        private readonly FinaciasPersonales _context;

        public UserRepository(FinaciasPersonales context)
        {
            _context = context;
        }

        public async Task Create(User entity)
        {

            await _context.User.AddAsync(entity);
            await Save();
        }

        public async Task Delete(User entity)
        {
            _context.User.Remove(entity);
           await  Save();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<User> GetByDNIAsync(string dni)
        {
            return await _context.User.Where(u => u.DNI == dni)
                     .FirstOrDefaultAsync();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.User.Where(u => u.Email == email)
                    .FirstOrDefaultAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _context.User.FindAsync(id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(User entity)
        {
            _context.User.Update(entity);
            await Save();
        }


    }
}

