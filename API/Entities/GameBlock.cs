using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public abstract class GameBlock
    {
        public int GameBlockId { get; set; }
        public int GamePlanId { get; set; }
        public int HeroId { get; set; }

        //References
        public virtual ICollection<Hero> Heroes { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual GamePlan GamePlan { get; set; }
    }
}
