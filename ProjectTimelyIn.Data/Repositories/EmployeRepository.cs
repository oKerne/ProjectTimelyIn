using ProjectTimelyIn.Data;
using Microsoft.EntityFrameworkCore;
using ProjectTimelyIn.Core.Entities;
using ProjectTimelyIn.Core.Repositorys;


namespace ProjectTimelyIn.Dtat.Repositories
{
    public class EmployeRepository : IEmployeeRepository
    {
        private readonly DataContext _context;
        public EmployeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _context.Employees.Include(u => u.WorkHours).ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> UpdateAsync(Employee employee)
        {
            var existingUser = await GetByIdAsync((int)employee.Id);
            if (existingUser == null)
            {
                throw new Exception("Employee not found");
            }

            existingUser.Email = employee.Email;

            await _context.SaveChangesAsync();
            return existingUser;
        }

        public async Task DeleteAsync(int id)
        {
            var existingUser = await _context.Employees.FindAsync(id);
            if (existingUser != null)
            {
                _context.Employees.Remove(existingUser);
                await _context.SaveChangesAsync();
            }
        }
    }
}

