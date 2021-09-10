using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.Domain.Entities
{
    public abstract class Block
    {
        public int BlockId { get; set; }
        public int SessionId { get; set; }
        public int BlockPosition { get; set; }
        
        //References
        public virtual ICollection<Hero> Heroes { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}
