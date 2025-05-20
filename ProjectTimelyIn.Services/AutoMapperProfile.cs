using AutoMapper;
using ProjectTimelyIn.Core.DTOS;
using ProjectTimelyIn.Core.Entities;

namespace ProjectTimelyIn.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Employee mappings
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));

            // WorkHours mappings
            CreateMap<WorkHours, WorkHoursDTO>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.FullName))
                .ForMember(dest => dest.WorkDate, opt => opt.MapFrom(src => src.StartTime.Date))
                .ForMember(dest => dest.TotalHours, opt => opt.MapFrom(src => (src.EndTime - src.StartTime).TotalHours));

            // Vacation mappings
            CreateMap<Vacation, VacationDTO>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.FullName));
        }
    }
}
