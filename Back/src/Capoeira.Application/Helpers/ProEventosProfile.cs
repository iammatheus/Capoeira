using AutoMapper;
using Capoeira.Domain;
using Capoeira.Application.Dtos;
using Capoeira.Domain.Identity;

namespace Capoeira.API.Helpers
{
    public class CapoeiraProfile : Profile
    {
        public CapoeiraProfile()
        {
            CreateMap<Evento, EventoDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
        }
    }
}