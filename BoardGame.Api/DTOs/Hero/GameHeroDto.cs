using BoardGame.Domain.Entities.EntityEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.Api.DTOs.Hero
{
    public class GameHeroDto
    {
        public int HeroId { get; set; }
        public int BlockId { get; set; }
        public int AppUserId { get; set; }
        public int HeroTypeId { get; set; }
        public string HeroName { get; set; }
        public HeroType HeroType { get; set; }
        public string ImagePath { get; set; }
        public int Level { get; set; }
        public int Lives { get; set; }
        public int LivesCap { get; set; }
        public int DmgMin { get; set; }
        public int DmgMax { get; set; }
        public int Armor { get; set; }
        public int Experience { get; set; }
        public int ExperienceCap { get; set; }
        public int SkillPoints { get; set; }
    }
}
