using Microsoft.AspNetCore.Mvc;
using ProjectTimelyIn.Core.Entities;
using AutoMapper;
using ProjectTimelyIn.Core.DTOS;
using Microsoft.AspNetCore.Authorization;
using ProjectTimelyIn.Services.Implementations;
using ProjectTimelyIn.API.Models;
using ProjectTimelyIn.Core.Repositorys;

namespace ProjectTimelyIn.Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService _employeeService;
        private readonly IMapper _mapper;
        public EmployeesController(EmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _employeeService.GetListAsync();
            var employeeDTOs = _mapper.Map<List<EmployeeDTO>>(employees); 
            return Ok(employeeDTOs); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "Invalid ID" });
            }

            var employee = await _employeeService.GetByIdAsync(id);  
            if (employee == null)
            {
                return NotFound(new { Message = "Employee not found" });
            }

            var employeeDTO = _mapper.Map<EmployeeDTO>(employee);  
            return Ok(employeeDTO);  
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeePostModel employeePostModel)
        {
            if (employeePostModel == null)
            {
                return BadRequest(new { Message = "Invalid employee data" });
            }

            var newEmployee = new Employee
            {
                FullName = $"{employeePostModel.FirstName} {employeePostModel.LastName}",
                Email = employeePostModel.Email,
                Role = employeePostModel.RoleId == 1 ? "Admin" : "Employee", 
                Password = employeePostModel.PasswordHash
            };
            var createdEmployee = await _employeeService.AddAsync(newEmployee);

            var employeeDTO = _mapper.Map<EmployeeDTO>(createdEmployee);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = createdEmployee.Id }, employeeDTO);
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee newEmployee)
        {
            if (newEmployee == null || id <= 0)
            {
                return BadRequest(new { Message = "Invalid employee data" });
            }

            var existingEmployee = await _employeeService.GetByIdAsync(id);
            if (existingEmployee == null)
            {
                return NotFound(new { Message = "Employee not found" });
            }

            existingEmployee.FullName = newEmployee.FullName;
            existingEmployee.Email = newEmployee.Email;
            existingEmployee.Role = newEmployee.Role;

            var updatedEmployee = await _employeeService.UpdateAsync(existingEmployee);

            var employeeDTO = _mapper.Map<EmployeeDTO>(updatedEmployee);
            return Ok(employeeDTO);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);

            if (employee == null || id < 0)
            {
                return NotFound(new { Message = "Employee not found" });
            }
            await _employeeService.DeleteAsync(id);
            return NoContent();
        }
    }
}
