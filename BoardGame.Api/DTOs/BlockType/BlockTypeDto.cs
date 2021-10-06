using BoardGame.Domain.Entities.EntityEnums;

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
