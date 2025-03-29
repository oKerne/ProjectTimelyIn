using Microsoft.EntityFrameworkCore;
using ProjectTimelyIn.Core.Entities;
using ProjectTimelyIn.Core.Repositorys;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectTimelyIn.Data.Repositories
{
    public class WorkHoursRepository : IWorkHoursRepository
    {
        private readonly DataContext _dataContext;

        public WorkHoursRepository(DataContext context)
        {
            _dataContext = context;
        }

        public async Task<List<WorkHours>> GetAllAsync() 
        {
            return await _dataContext.WorkHours.ToListAsync();
        }

        public async Task<List<WorkHours>> GetWorkHoursAsync(int employeeId)
        {
            return await _dataContext.WorkHours
                .Where(x => x.EmployeeId == employeeId)
                .ToListAsync();
        }

        public async Task<WorkHours> AddAsync(WorkHours workHours) 
        {
            _dataContext.WorkHours.Add(workHours);
            await _dataContext.SaveChangesAsync();
            return workHours;
        }

        public async Task<WorkHours> UpdateAsync(WorkHours workHours) 
        {
            var existingWorkHours = await _dataContext.WorkHours.FindAsync(workHours.Id);
            if (existingWorkHours == null)
            {
                throw new Exception("WorkHours not found");
            }

            _dataContext.Entry(existingWorkHours).CurrentValues.SetValues(workHours);
            await _dataContext.SaveChangesAsync();
            return existingWorkHours;
        }

        public async Task<bool> DeleteAsync(int id) 
        {
            var existingWorkHours = await _dataContext.WorkHours.FindAsync(id);
            if (existingWorkHours != null)
            {
                _dataContext.WorkHours.Remove(existingWorkHours);
                await _dataContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
