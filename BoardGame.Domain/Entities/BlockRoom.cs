using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.Domain.Entities
{
    public class BlockRoom:Block
    {
        public int? MonsterId { get; set; }

        //Reference
        public virtual Monster Monster { get; set; }
    }
}
