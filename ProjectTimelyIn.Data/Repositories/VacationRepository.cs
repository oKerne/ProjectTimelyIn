
using Microsoft.EntityFrameworkCore;
using ProjectTimelyIn.Core.Entities;
using ProjectTimelyIn.Core.Repositorys;

namespace ProjectTimelyIn.Data.Repositories
{
    public class VacationRepository : IVacationRepository
    {
        private readonly DataContext _dataContext;

        public VacationRepository(DataContext context)
        {
            _dataContext = context;
        }

        public async Task<List<Vacation>> GetAllAsync()
        {
            return await _dataContext.Vacations.ToListAsync();
        }

        public async Task<Vacation> GetByIdAsync(int id)
        {
            return await _dataContext.Vacations.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Vacation> AddAsync(Vacation vacation)
        {
            _dataContext.Vacations.Add(vacation);
            await _dataContext.SaveChangesAsync();
            return vacation;
        }

        public async Task<Vacation> UpdateAsync(Vacation vacation)
        {
            var existingVacation = await GetByIdAsync(vacation.Id);
            if (existingVacation == null)
            {
                throw new Exception("Vacation not found");
            }

            existingVacation.EmployeeId = vacation.EmployeeId;
            existingVacation.StartDate = vacation.StartDate;
            existingVacation.EndDate = vacation.EndDate;
            existingVacation.Reason = vacation.Reason;

            await _dataContext.SaveChangesAsync();
            return existingVacation;
        }

        public async Task<List<Vacation>> GetByEmployeeIdAsync(int employeeId)
        {
            return await _dataContext.Vacations
                .Where(v => v.EmployeeId == employeeId)
                .ToListAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingVacation = await _dataContext.Vacations.FindAsync(id);
            if (existingVacation != null)
            {
                _dataContext.Vacations.Remove(existingVacation);
                await _dataContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
