﻿using API.DTOs.Session;

using AutoMapper;
using BoardGame.Api.DTOs.BattleReportDto;
using BoardGame.Api.DTOs.Block;
using BoardGame.Api.DTOs.BlockType;
using BoardGame.Api.DTOs.Hero;
using BoardGame.Api.DTOs.MessageDto;
using BoardGame.Api.DTOs.Monster;
using BoardGame.Api.DTOs.MonsterType;
using BoardGame.Api.DTOs.Session;
using BoardGame.Domain.Models;

namespace API.DtoMappers
{
    public class DtoMapperProfile : Profile
    {
        public DtoMapperProfile()
        {
            CreateMap<SessionModel, SessionDto>().ReverseMap();
            CreateMap<CreateSessionDto, SessionModel>();
            CreateMap<SessionModel, GameSessionDto>().ReverseMap();
            CreateMap<BlockModel, GameBlockDto>().ReverseMap();
            CreateMap<HeroModel, GameHeroDto>().ReverseMap();
            CreateMap<ChatMessageModel, MessageDto>().ReverseMap();
            CreateMap<BlockTypeModel, BlockTypeDto>().ReverseMap();
            CreateMap<ActivePlayerModel, ActivePlayerDto>().ReverseMap();
            CreateMap<MonsterModel, MonsterOnBoardDto>().ReverseMap();
            CreateMap<MonsterTypeModel, MonsterTypeDto>().ReverseMap();
            CreateMap<BattleReportModel, BattleReportDto>().ReverseMap();
        }
    }
}
