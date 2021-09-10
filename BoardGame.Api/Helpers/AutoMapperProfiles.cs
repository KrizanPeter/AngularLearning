using API.DTOs;
using API.DTOs.Session;
using API.Entities;
using AutoMapper;
using BoardGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Session, SessionDto>().ReverseMap();
            CreateMap<CreateSessionDto, Session>();
        }
    }
}
