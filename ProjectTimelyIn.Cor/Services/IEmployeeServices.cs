using ProjectTimelyIn.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTimelyIn.Core.Services
{
    public interface IEmployeeServices
    {
            Task<List<Employee>> GetAllAsync();
            Task<Employee> GetByIdAsync(int id);
            Task<Employee> AddAsync(Employee employee);
            Task<Employee> UpdateAsync(Employee employee);
            Task DeleteAsync(int id);

    }
}
