﻿using System.Collections.Generic;

using BoardGame.Api.DTOs.BlockType;
using BoardGame.Api.DTOs.Hero;
using BoardGame.Api.DTOs.Monster;

namespace BoardGame.Api.DTOs.Block
{
    public class GameBlockDto
    {
        public int BlockId { get; set; }
        public int SessionId { get; set; }
        public int? MonsterId { get; set; }
        public int BlockTypeId { get; set; }
        public int BlockPositionX { get; set; }
        public int BlockPositionY { get; set; }
        public int BlockOrder { get; set; }
        public BlockTypeDto BlockType { get; set; }
        public string IncomingMovement { get; set; }
        public MonsterOnBoardDto Monster { get; set; }

        public ICollection<GameHeroDto> Heroes { get; set; }

    }
}
