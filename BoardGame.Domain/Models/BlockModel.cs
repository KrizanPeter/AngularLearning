using System.Collections.Generic;

namespace BoardGame.Domain.Models
{
    public class BlockModel
    {
        public int BlockId { get; set; }
        public int SessionId { get; set; }
        public int? MonsterId { get; set; }
        public int BlockTypeId { get; set; }
        public int BlockPositionX { get; set; }
        public int BlockPositionY { get; set; }
        public int BlockOrder { get; set; }
        public BlockTypeModel BlockType { get; set; }
        public string IncomingMovement { get; set; }

        public ICollection<HeroModel> Heroes { get; set; }

    }
}
