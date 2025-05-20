using ProjectTimelyIn.Core.Entities;
using ProjectTimelyIn.Core.Repositorys;
using ProjectTimelyIn.Core.Services;
using System;
using System.Threading.Tasks;

namespace ProjectTimelyIn.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private bool _disposed = false;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<int> AddUserAsync(ProjectTimelyIn.Core.Entities.User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            // In a real application, you would hash the password here
            // user.Password = HashPassword(user.Password);
            
            return await _userRepository.AddUserAsync(user);
        }

        public async Task<ProjectTimelyIn.Core.Entities.User?> GetUserByCredentialsAsync(string firstName, string lastName, string password)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name cannot be empty", nameof(firstName));
                
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be empty", nameof(lastName));
                
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty", nameof(password));

            // In a real application, you would hash the password before checking
            // var hashedPassword = HashPassword(password);
            return await _userRepository.GetUserByCredentialsAsync(firstName, lastName, password);
        }

        public async Task<ProjectTimelyIn.Core.Entities.User?> GetUserByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero");
                
            return await _userRepository.GetUserByIdAsync(id);
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
                    _userRepository.Dispose();
                }
                _disposed = true;
            }
        }

        // In a real application, implement proper password hashing
        /*
        private static string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
        */
    }
}
