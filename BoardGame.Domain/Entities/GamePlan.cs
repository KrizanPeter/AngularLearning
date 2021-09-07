using BoardGame.Domain.Entities.EntityEnums;
using System.Collections.Generic;

namespace BoardGame.Domain.Entities
{
    public class GamePlan
    {
        public int GamePlanId { get; set; }
        public PlanSize PlanSize { get; set; }

        //References

        public virtual ICollection<GameBlock> GameBlocks { get; set; }
    }
}
