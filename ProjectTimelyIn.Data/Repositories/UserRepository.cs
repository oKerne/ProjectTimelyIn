using Microsoft.EntityFrameworkCore;
using ProjectTimelyIn.Core.Entities;
using ProjectTimelyIn.Core.Repositorys;
using System;
using System.Threading.Tasks;

namespace ProjectTimelyIn.Data.Repositories
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private readonly DataContext _context;
        private bool _disposed = false;

        public UserRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<User?> GetUserByCredentialsAsync(string firstName, string lastName, string password)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name cannot be empty", nameof(firstName));
                
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be empty", nameof(lastName));
                
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty", nameof(password));

            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => 
                    u.FirstName == firstName && 
                    u.LastName == lastName && 
                    u.Password == password);
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero");

            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<int> AddUserAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            
            return user.Id;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }
    }
}
