using API.Entities.EntityEnums;
using System.Collections.Generic;

namespace API.Entities
{
    public class GameSession
    {
        public int  GameSessionId { get; set; }
        public int GamePlanId { get; set; }
        public string SessionName { get; set; }
        public string SessionPassword { get; set; }
        public SessionType SessionType { get; set; }

        //Reference

        public virtual GamePlan GamePlan { get; set; }
        public virtual ICollection<AppUser> Users { get; set; }
    }
}