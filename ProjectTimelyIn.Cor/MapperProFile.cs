using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTimelyIn.Core.DTOS;
using ProjectTimelyIn.Core.Entities;

namespace ProjectTimelyIn.Core
{
     public class MapperProFile : Profile
    {
        public MapperProFile()
        {
            CreateMap<Employee, EmployeeDTO>().ReverseMap()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                 .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName));
            //CreateMap<EmployeePostModel, Employee>()
            // .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => HashPassword(src.Password)));
            CreateMap<Vacation, VacationDTO>().ReverseMap()
             .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
             .ForMember(dest => dest.Employee, opt => opt.MapFrom(src => src.Employee != null ? src.Employee.FullName : string.Empty));  

            CreateMap<WorkHours, WorkHoursDTO>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.FullName ?? ""))
                 .ForMember(dest => dest.TotalHours, opt => opt.MapFrom(src => (src.EndTime - src.StartTime).TotalHours));
        }
     }

    
}
