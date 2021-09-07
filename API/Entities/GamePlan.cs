using API.Entities.EntityEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class GamePlan
    {
        public int GamePlanId { get; set; }
        public PlanSize PlanSize { get; set; }

        //References

        public virtual ICollection<GameBlock> GameBlocks { get; set; }
    }
}
