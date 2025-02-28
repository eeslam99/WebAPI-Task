using AutoMapper;
using WebAPI02.Models;

namespace WebAPI02.Helpers
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, DTOs.StudentDTO>()
                .ForMember(d => d.DepartmentName, opt => opt.MapFrom(s => s.Dept.DeptName))
                .ForMember(d => d.SupervisorName, opt => opt.MapFrom(s => $"{s.StSuperNavigation.StFname} {s.StSuperNavigation.StLname}"))
                .ReverseMap();

            CreateMap<Department, DTOs.DepartmentDTO>()
                .ForMember(d => d.StudentsCount, opt => opt.MapFrom(s => s.Students.Count))
                .ReverseMap();
        }
    }
}
