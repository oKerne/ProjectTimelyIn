using ProjectTimelyIn.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTimelyIn.Core.Repositorys
{
    public interface IWorkHoursRepository
    {
        Task<List<WorkHours>> GetAllAsync();
        Task<List<WorkHours>> GetWorkHoursAsync(int employeeId);
        Task<WorkHours> AddAsync(WorkHours workHours);
        Task<WorkHours> UpdateAsync(WorkHours workHours);
        Task<bool> DeleteAsync(int id);
    }
}
