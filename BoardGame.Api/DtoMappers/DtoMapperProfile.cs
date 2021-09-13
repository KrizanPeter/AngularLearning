using API.DTOs.Session;
using AutoMapper;
using BoardGame.Domain.Models;

namespace API.DtoMappers
{
    public class DtoMapperProfile : Profile
    {
        public DtoMapperProfile()
        {

            CreateMap<SessionModel, SessionDto>().ReverseMap();
            CreateMap<CreateSessionDto, SessionModel>();
        }
    }
}
