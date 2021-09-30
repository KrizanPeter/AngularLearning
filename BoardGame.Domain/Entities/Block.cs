using System.Collections.Generic;

using BoardGame.Domain.Entities.EntityEnums;

namespace BoardGame.Domain.Entities
{
    public class Block
    {
        public int BlockId { get; set; }
        public int SessionId { get; set; }
        public int? MonsterId { get; set; }
        public int BlockPositionX { get; set; }
        public int BlockPositionY { get; set; }
        public int BlockOrder { get; set; }
        public BlockType BlockType { get; set; }
        public BlockDirection BlockDirection{get; set;}
        public string ImagePath { get; set; }

        //Reference
        public virtual Monster Monster { get; set; }
        public virtual ICollection<Hero> Heroes { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}
