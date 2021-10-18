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
        public int Level { get; set; }
        public int Lives { get; set; } = 10;
        public int DmgMin { get; set; } = 2;
        public int DmgMax { get; set; } = 12;
        public int Armor { get; set; } = 0;

        //References
        public virtual AppUser User { get; set; }
        //public virtual ICollection<Item> Items { get; set; }
        //public virtual HeroType HeroType { get; set; }
    }
}
