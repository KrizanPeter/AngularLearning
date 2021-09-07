using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.Domain.Entities
{
    public class Hero
    {
        public int HeroId { get; set; }
        public int GameBlockId { get; set; }
        public int AppUserId { get; set; }
        public int HeroTypeId { get; set; }
        public string HeroName { get; set; }
        public int Lives { get; set; }

        //References
        public virtual AppUser User { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual HeroType HeroType { get; set; }
        public GameBlock GabeBlock { get; set; }
    }
}
