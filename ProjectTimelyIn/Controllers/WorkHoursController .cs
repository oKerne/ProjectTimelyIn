
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectTimelyIn.Core.DTOS;
using ProjectTimelyIn.Core.Entities;
using ProjectTimelyIn.Core.Services;
using System.Security.Claims;

namespace ProjectTimelyIn.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkHoursController : ControllerBase
    {
        private readonly IWorkHoursServices _workHoursService;
        private readonly IMapper _mapper;

        public WorkHoursController(IWorkHoursServices workHoursService, IMapper mapper)
        {
            _workHoursService = workHoursService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWorkHours()
        {
            var workHours = await _workHoursService.GetAllAsync();
            var workHoursDTOs = _mapper.Map<List<WorkHoursDTO>>(workHours);
            return Ok(workHoursDTOs);
        }

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetWorkHoursAsync(int employeeId)
        {
            var workHours = await _workHoursService.GetWorkHoursAsync(employeeId);

            if (workHours == null || !workHours.Any())
            {
                return NotFound(new { Message = "No work hours found for this employee" });
            }

            var workHoursDTOs = _mapper.Map<List<WorkHoursDTO>>(workHours);
            return Ok(workHoursDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> AddWorkHours([FromBody] WorkHours workHours)
        {
            if (workHours == null)
            {
                return BadRequest(new { Message = "Invalid work hours data" });
            }

            var createdWorkHours = await _workHoursService.AddAsync(workHours);
            var workHoursDTO = _mapper.Map<WorkHoursDTO>(createdWorkHours);

            return CreatedAtAction(nameof(GetWorkHoursAsync), new { employeeId = createdWorkHours.EmployeeId }, workHoursDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkHours(int id, [FromBody] WorkHours workHours)
        {
            if (workHours == null || id <= 0)
            {
                return BadRequest(new { Message = "Invalid data" });
            }

            var updatedWorkHours = await _workHoursService.UpdateAsync(workHours);
            var workHoursDTO = _mapper.Map<WorkHoursDTO>(updatedWorkHours);

            return Ok(workHoursDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkHours(int id)
        {
            var workHour = await _workHoursService.GetWorkHoursAsync(id);

            if (workHour == null)
                return NotFound(new { Message = "Work hours entry not found" });

            var employeeId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            if (workHour.FirstOrDefault()?.EmployeeId != employeeId)
                return Forbid();

            var deleted = await _workHoursService.DeleteAsync(id);
            return deleted ? NoContent() : StatusCode(500, new { Message = "Error deleting work hours" });
        }
    }
}
