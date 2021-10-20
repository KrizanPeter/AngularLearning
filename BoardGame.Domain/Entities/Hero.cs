using BoardGame.Domain.Entities.EntityEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.Domain.Entities
{
    public class Hero
    {
        public int HeroId { get; set; }
        public int BlockId { get; set; }
        public int AppUserId { get; set; }
        public int HeroTypeId { get; set; }
        public string HeroName { get; set; }
        public HeroType HeroType { get; set; }
        public string ImagePath { get; set; }
        public int Level { get; set; } = 1;
        public int Lives { get; set; } = 100;
        public int LivesCap { get; set; } = 100;
        public int DmgMin { get; set; } = 2;
        public int DmgMax { get; set; } = 12;
        public int Armor { get; set; } = 0;
        public int Experience { get; set; } = 0;
        public int ExperienceCap { get; set; } = 100;
        public int SkillPoints { get; set; } = 3;

        //References
        public virtual AppUser User { get; set; }
        //public virtual ICollection<Item> Items { get; set; }
        //public virtual HeroType HeroType { get; set; }
    }
}
