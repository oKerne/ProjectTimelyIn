


using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectTimelyIn.Core.DTOS;

using ProjectTimelyIn.Core.Entities;
using ProjectTimelyIn.Core.Repositorys;
using ProjectTimelyIn.Dtat.Repositories;
using ProjectTimelyIn.Core.Services;
using System.Security.Claims;
using ProjectTimelyIn.Api.Models;

namespace ProjectTimelyIn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacationsController : ControllerBase
    {
        private readonly IVacationRepository _vacationRepository;
        private readonly IVacationServices _vacationService;

        private readonly IMapper _mapper;

        public VacationsController(IVacationRepository vacationRepository,IMapper mapper, IVacationServices vacationService)
        {
            _vacationRepository = vacationRepository;
            _mapper = mapper;
            _vacationService = vacationService;
        }
        [HttpGet]
        public async Task<IActionResult> GetVacationRequests()
        {
            var vacationRequests = await _vacationService.GetAllAsync();
            var vacationRequestDTOs = _mapper.Map<List<VacationDTO>>(vacationRequests);
            return Ok(vacationRequestDTOs);
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> GetVacationByEmployee(int employeeId)
        {
            var vacation = await _vacationService.GetByEmployeeIdAsync(employeeId);
            if (vacation == null || vacation.Count == 0)
            {
                return NotFound(new { Message = "Vacation requests not found for this employee." });
            }

            var vacationDTOs = _mapper.Map<List<VacationDTO>>(vacation);
            return Ok(vacationDTOs);
        }

        [HttpPost]
        //public async Task<IActionResult> CreateVacation([FromBody] Vacation newVacation)
        //{
        //    if (newVacation == null)
        //    {
        //        return BadRequest(new { Message = "Invalid vacation request data." });
        //    }

        //    var createdVacation = await _vacationRepository.AddAsync(newVacation);
        //    var vacationRequestDTO = _mapper.Map<VacationRequestDTO>(createdVacation);
        //    return CreatedAtAction(nameof(GetVacationRequestsByEmployee), new { employeeId = createdVacation.EmployeeId }, vacationRequestDTO);
        //}
        [HttpPost]
        public async Task<IActionResult> CreateVacation([FromBody] VacationPostModel vacationPostModel)
        {
            if (vacationPostModel == null)
            {
                return BadRequest(new { Message = "Invalid vacation data" });
            }

            var newVacation = new Vacation
            {
                StartDate = vacationPostModel.StartDate,
                EndDate = vacationPostModel.EndDate,
                Reason = vacationPostModel.Reason,
                EmployeeId = vacationPostModel.EmployeeId
            };

            var createdVacation = await _vacationService.AddVacationAsync(newVacation);

            var vacationDTO = _mapper.Map<VacationDTO>(createdVacation);
            return CreatedAtAction(nameof(GetVacationByEmployee), new { employeeId = createdVacation.EmployeeId }, vacationDTO);
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
            var vacationRequestDTO = _mapper.Map<VacationDTO>(updated);
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

