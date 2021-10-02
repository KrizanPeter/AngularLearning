using BoardGame.Domain.Entities.EntityEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.Api.DTOs.BlockType
{
    public class BlockTypeDto
    {
        public int BlockTypeId { get; set; }
        public string ImageName { get; set; }
        public bool ExitTop { get; set; }
        public bool ExitDown { get; set; }
        public bool ExitRight { get; set; }
        public bool ExitLeft { get; set; }
        public BlockCategory BlockCategory { get; set; }
    }
}
