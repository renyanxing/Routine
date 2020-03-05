using AutoMapper;
using Routine.Api.Entities;
using Routine.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.firstName} {src.lastName}"))
                .ForMember(dest => dest.GenderDisplay, opt => opt.MapFrom(res => res.Gender.ToString()))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(res => DateTime.Now.Year - res.dateOfBirth.Year));
            CreateMap<EmployeeAddDto, Employee>();
            CreateMap<EmployeeUpdateDto, Employee>();
            CreateMap<Employee, EmployeeUpdateDto>();

        }
    }
}
