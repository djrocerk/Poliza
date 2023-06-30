using API.DTOs;
using AutoMapper;
using Domain.Entities;

namespace API.Profiles
{
    public class PolizaProfile : Profile
    {
        public PolizaProfile()
        {
            CreateMap<CrearPolizaReq, Poliza>();
            CreateMap<Poliza, PolizaResponse>();
        }
    }
}
