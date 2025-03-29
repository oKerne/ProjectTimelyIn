using ProjectTimelyIn.Core.Entities;
namespace ProjectTimelyIn.Core.Services
{
   public interface IVacationServices
    {     
        Task<List<Vacation>> GetAllAsync();
        Task<Vacation> GetByIdAsync(int id);
        Task<List<Vacation>> GetByEmployeeIdAsync(int employeeId);
        Task<Vacation> AddVacationAsync(Vacation vacation);
        Task<Vacation> UpdateVacationAsync(Vacation vacation);
        Task DeleteVacationAsync(int id);
    }
    

}
