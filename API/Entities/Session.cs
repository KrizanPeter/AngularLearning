using API.Entities.EntityEnums;
using System.Collections.Generic;

namespace API.Entities
{
    public class Session
    {
        public int  SessionId { get; set; }
        public int GamePlanId { get; set; }
        public string SessionName { get; set; }
        public string SessionPassword { get; set; }
        public SessionType SessionType { get; set; }

        //Reference

        public GamePlan GamePlan { get; set; }
    }
}