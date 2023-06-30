using API.DTOs;
using AutoMapper;
using Domain.Entities;

namespace API.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<CrearUsuarioReq, Usuario>();
        }
    }
}
