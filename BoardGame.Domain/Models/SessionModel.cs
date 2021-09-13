using BoardGame.Domain.Entities.EntityEnums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.Domain.Models
{
    public class SessionModel
    {
        public int SessionId { get; set; }
        public string SessionName { get; set; }
        public string SessionPassword { get; set; }
        public SessionType SessionType { get; set; }
        public PlanSize PlanSize { get; set; }
        public int CenterBlockPosition { get; set; }

        public ICollection<BlockModel> Blocks { get; set; } 
    }
}
