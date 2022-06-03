using Employees.Dtos;
using AutoMapper;

namespace Employees.Models
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<EmployeeDto, Employee>().ReverseMap();
            CreateMap<BeneficiaryDto, Beneficiary>().ReverseMap();
            CreateMap<SingInRequestDto, SingIn>().ReverseMap();
        }
    }
}
