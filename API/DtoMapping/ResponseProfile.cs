using AutoMapper;
using EasyApply.Core.Domian;
using EasyApply.Dto;

namespace API.DtoMapping
{
    public class ResponseProfile : Profile
    {
        public ResponseProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap();
           
        }
    }
}
