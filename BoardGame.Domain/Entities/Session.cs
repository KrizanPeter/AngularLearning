using System.Collections.Generic;

using BoardGame.Domain.Entities.EntityEnums;

namespace BoardGame.Domain.Entities
{
    public class Session
    {
        public int SessionId { get; set; }
        public string SessionName { get; set; }
        public string SessionPassword { get; set; }
        public SessionType SessionType { get; set; }
        public PlanSize PlanSize { get; set; }
        public int CenterBlockPosition { get; set; }
        public int? CurrentPlayerId { get; set; }

        //References

        public ICollection<Block> Blocks { get; set; }
        //public virtual ICollection<AppUser> Users { get; set; }
    }
}