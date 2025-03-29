


using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectTimelyIn.Core.DTOS;
using ProjectTimelyIn.Core.Entities;
using ProjectTimelyIn.Core.Repositorys;
using System.Security.Claims;

namespace ProjectTimelyIn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacationsController : ControllerBase
    {
        private readonly IVacationRepository _vacationRepository;
        private readonly IMapper _mapper;

        public VacationsController(IVacationRepository vacationRepository, IMapper mapper)
        {
            _vacationRepository = vacationRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetVacationRequests()
        {
            var vacationRequests = await _vacationRepository.GetAllAsync();
            var vacationRequestDTOs = _mapper.Map<List<VacationRequestDTO>>(vacationRequests);
            return Ok(vacationRequestDTOs);
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> GetVacationRequestsByEmployee(int employeeId)
        {
            var vacation = await _vacationRepository.GetByEmployeeIdAsync(employeeId);
            if (vacation == null || vacation.Count == 0)
            {
                return NotFound(new { Message = "Vacation requests not found for this employee." });
            }

            var vacationDTOs = _mapper.Map<List<VacationRequestDTO>>(vacation);
            return Ok(vacationDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVacation([FromBody] Vacation newVacation)
        {
            if (newVacation == null)
            {
                return BadRequest(new { Message = "Invalid vacation request data." });
            }

            var createdVacation = await _vacationRepository.AddAsync(newVacation);
            var vacationRequestDTO = _mapper.Map<VacationRequestDTO>(createdVacation);
            return CreatedAtAction(nameof(GetVacationRequestsByEmployee), new { employeeId = createdVacation.EmployeeId }, vacationRequestDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVacationRequest(int id, [FromBody] Vacation updatedVacationRequest)
        {
            if (updatedVacationRequest == null || id <= 0)
            {
                return BadRequest(new { Message = "Invalid data." });
            }

            var existingVacationRequest = await _vacationRepository.GetByIdAsync(id);
            if (existingVacationRequest == null)
            {
                return NotFound(new { Message = "Vacation request not found." });
            }

            existingVacationRequest.StartDate = updatedVacationRequest.StartDate;
            existingVacationRequest.EndDate = updatedVacationRequest.EndDate;
            existingVacationRequest.Reason = updatedVacationRequest.Reason;

            var updated = await _vacationRepository.UpdateAsync(existingVacationRequest);
            var vacationRequestDTO = _mapper.Map<VacationRequestDTO>(updated);
            return Ok(vacationRequestDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVacation(int id)
        {
            var vacation = await _vacationRepository.GetByIdAsync(id);
            if (vacation == null)
                return NotFound(new { Message = "Vacation entry not found" });

            var employeeId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (vacation.EmployeeId != employeeId)
                return Forbid();

            var deleted = await _vacationRepository.DeleteAsync(id);
            return deleted ? NoContent() : StatusCode(500, new { Message = "Error deleting vacation" });
        }
    }
}

