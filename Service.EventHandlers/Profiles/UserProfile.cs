using AutoMapper;
using Domain.Entities;
using Service.EventHandlers.Commands;
using Service.EventHandlers.Responses;

namespace Service.EventHandlers.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreateCommand, Usuario>();
            CreateMap<Usuario, UserInfoResult>();
        }
    }
}
