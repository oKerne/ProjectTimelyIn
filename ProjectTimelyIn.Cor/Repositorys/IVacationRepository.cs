using ProjectTimelyIn.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTimelyIn.Core.Repositorys
{
    public interface IVacationRepository
    {
            Task<List<Vacation>> GetAllAsync();
            Task<Vacation> AddAsync(Vacation vacation);
            Task<Vacation> UpdateAsync(Vacation vacation);
            Task<List<Vacation>> GetByEmployeeIdAsync(int employeeId);
            Task<Vacation> GetByIdAsync(int id);
            Task<bool> DeleteAsync(int id);
    }
}
