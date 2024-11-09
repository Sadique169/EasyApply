using AutoMapper;
using EasyApply.Core.Domian;
using EasyApply.Dto;
using EasyApply.Model;

namespace API.DtoMapping
{
    public class ResponseProfile : Profile
    {
        public ResponseProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap();

            CreateMap<Company, CompanyModel>().ReverseMap();
            CreateMap<Company, CompanyDto>().ReverseMap();
        }
    }
}
