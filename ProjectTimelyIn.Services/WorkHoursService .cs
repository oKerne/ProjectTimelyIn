using AutoMapper;
using ProjectTimelyIn.Core.Services;
using ProjectTimelyIn.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectTimelyIn.Core.Repositorys;

namespace ProjectTimelyIn.Services
{
    public class WorkHoursService : IWorkHoursServices
    {
        private readonly IWorkHoursRepository _workHoursRepository;
        private readonly IMapper _mapper;

        public WorkHoursService(IWorkHoursRepository workHoursRepository, IMapper mapper)
        {
            _workHoursRepository = workHoursRepository;
            _mapper = mapper;
        }

        public async Task<List<WorkHours>> GetAllAsync()
        {
            try
            {
                return await _workHoursRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve all work hours", ex);
            }
        }

        public async Task<List<WorkHours>> GetWorkHoursAsync(int employeeId)
        {
            try
            {
                return await _workHoursRepository.GetWorkHoursAsync(employeeId);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve work hours", ex);
            }
        }

        public async Task<WorkHours> AddAsync(WorkHours workHours)
        {
            try
            {
                return await _workHoursRepository.AddAsync(workHours);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add work hours", ex);
            }
        }

        public async Task<WorkHours> UpdateAsync(WorkHours workHours)
        {
            try
            {
                return await _workHoursRepository.UpdateAsync(workHours);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update work hours", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _workHoursRepository.DeleteAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete work hours", ex);
            }
        }
    }
}
