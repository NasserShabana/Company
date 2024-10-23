using AutoMapper;
using Company.G05.PL.ViewModels.Employees;
using Company.G05.DAL.Models;

namespace Company.G05.PL.Mapping.Employees
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
        }
    }
}
