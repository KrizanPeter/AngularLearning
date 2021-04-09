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
        public int ItemId { get; set; }

        //References
        public Hero Hero { get; set; }
        public Item Item { get; set; }
        public GamePlan GamePlan { get; set; }
    }
}
