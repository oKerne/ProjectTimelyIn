using ProjectTimelyIn.Core.Entities;
using System;
using System.Threading.Tasks;

namespace ProjectTimelyIn.Core.Repositorys
{
    public interface IUserRepository : IDisposable
    {
        Task<User?> GetUserByCredentialsAsync(string firstName, string lastName, string password);
        Task<User?> GetUserByIdAsync(int id);
        Task<int> AddUserAsync(User user);
    }
}
