using AutoMapper;
using fooddotcomapi.Authorization;
using fooddotcomapi.Dto;

namespace fooddotcomapi.Helpers
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<ApplicationUser, RegisterDto>().ReverseMap();
            CreateMap<UpdateNameDto, ApplicationUser>();
            CreateMap<UpdatePhoneDto, ApplicationUser>();
        }
    }
}
