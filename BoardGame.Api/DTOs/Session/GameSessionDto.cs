using BoardGame.Api.DTOs.Block;
using BoardGame.Domain.Entities.EntityEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.Api.DTOs.Session
{
    public class GameSessionDto
    {
        public int SessionId { get; set; }
        public string SessionName { get; set; }
        public string SessionPassword { get; set; }
        public SessionType SessionType { get; set; }
        public PlanSize PlanSize { get; set; }
        public int CenterBlockPosition { get; set; }

        public ICollection<GameBlockDto> Blocks { get; set; }
        public ICollection<ICollection<GameBlockDto>> BlocksShape { get; set; }
    }
}
