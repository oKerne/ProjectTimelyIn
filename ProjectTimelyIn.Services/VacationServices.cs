using AutoMapper;
using ProjectTimelyIn.Core.Entities;
using ProjectTimelyIn.Core.Repositorys;
using ProjectTimelyIn.Core.Services;
using ProjectTimelyIn.Data.Repositories;

namespace ProjectTimelyIn.Services
{
    public class VacationServices : IVacationServices
    {
        private readonly VacationRepository _vacation;

        //private readonly IMapper _mapper;

        public VacationServices(VacationRepository vacationRepository, IMapper mapper)
        {
            _vacation = vacationRepository;
            //_mapper = mapper;
        }
        public async Task<List<Vacation>> GetAllAsync()
        {
            return await _vacation.GetAllAsync();
        }
        public async Task<Vacation> GetByIdAsync(int id)
        {
            return await _vacation.GetByIdAsync(id);
        }
        public async Task<List<Vacation>> GetByEmployeeIdAsync(int employeeId)
        {
            try
            {
                return await _vacation.GetByEmployeeIdAsync(employeeId);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve vacation requests", ex);
            }
        }


        public async Task<Vacation> AddVacationAsync(Vacation vacation)
        {
            try
            {
                return await _vacation.AddAsync(vacation);

            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add vacation request", ex);
            }
        }

        public async Task<Vacation> UpdateVacationAsync(Vacation vacation)
        {
            try
            {
                return await _vacation.UpdateAsync(vacation);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update vacation request", ex);
            }
        }

        public async Task DeleteVacationAsync(int id)
        {
            try
            {
                await _vacation.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete vacation request", ex);
            }
        }
    }
}
