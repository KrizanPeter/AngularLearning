using BoardGame.Domain.Entities.EntityEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.Api.DTOs.Block
{
    public class GameBlockDto
    {
        public int BlockId { get; set; }
        public int SessionId { get; set; }
        public int? MonsterId { get; set; }
        public int BlockPositionX { get; set; }
        public int BlockPositionY { get; set; }
        public int BlockOrder { get; set; }

        public BlockType BlockType { get; set; }
        public BlockDirection BlockDirection { get; set; }
    }
}
