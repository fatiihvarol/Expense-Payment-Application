using AutoMapper;
using Web.Data.Entity;
using WebSchema;

namespace Web.Business.Mapper;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<ApplicationUser, ApplicationUserResponse>();
        CreateMap<ApplicationUserRequest,ApplicationUser>();
    }
}