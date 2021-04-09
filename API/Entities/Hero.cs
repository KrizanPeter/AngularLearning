using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Hero
    {
        public int HeroId { get; set; }
        public int HeroTypeId { get; set; }
        public string HeroName { get; set; }
        public int Lives { get; set; }

        //References
        public List<Item> Items { get; set; }
        public HeroType HeroType { get; set; }

    }
}
