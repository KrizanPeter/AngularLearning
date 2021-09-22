using BoardGame.Domain.Entities.EntityEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.Api.DTOs.Hero
{
    public class PickHeroDto
    {
        public HeroType HeroType { get; set; }
    }
}
