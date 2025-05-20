
using AutoMapper;
using ProjectTimelyIn.Core.DTOS;
using ProjectTimelyIn.Core.Entities;
using ProjectTimelyIn.Core.Services;
using ProjectTimelyIn.Dtat.Repositories;
using ProjectTimelyIn.Core.Repositorys;


namespace ProjectTimelyIn.Services.Implementations
{
    public class EmployeeService : IEmployeeServices
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeRepository,IMapper mapper)
        {
            _employeeRepository = employeRepository;
            _mapper = mapper;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            try
            {
                return await _employeeRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve employee list", ex);
            }
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            try
            {
                return await _employeeRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve employee by ID", ex);
            }
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            try
            {
                return await _employeeRepository.AddAsync(employee);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add employee", ex);
            }
        }

        public async Task<Employee> UpdateAsync(Employee employee)
        {
            try
            {
                return await _employeeRepository.UpdateAsync(employee);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update employee", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _employeeRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete employee", ex);
            }
        }

        public async Task<List<EmployeeDTO>> GetEmployeesAsync()
        {
            try
            {
                var employees = await _employeeRepository.GetAllAsync();
                return _mapper.Map<List<EmployeeDTO>>(employees);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve employees", ex);
            }
        }

      
    }
}


