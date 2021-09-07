using API.Entities.EntityEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class GameSessionDto
    {
        public int GameSessionId { get; set; }
        public int GamePlanId { get; set; }
        public string SessionName { get; set; }
        public string SessionPassword { get; set; }
        public SessionType SessionType { get; set; }
    }
}
