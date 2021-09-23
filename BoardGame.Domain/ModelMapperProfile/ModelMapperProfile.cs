
using AutoMapper;
using BoardGame.Domain.Entities;
using BoardGame.Domain.Models;

namespace BoardGame.Domain.ModelMapperProfile
{
    public class ModelMapperProfile : Profile
    {
        public ModelMapperProfile()
        {
            CreateMap<Session, SessionModel>().ReverseMap();
            CreateMap<Block, BlockModel>().ReverseMap();
            CreateMap<Hero, HeroModel>().ReverseMap();

        }
    }
}
