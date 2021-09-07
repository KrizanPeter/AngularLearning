using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.Domain.Entities
{
    public class GameBlockRoom:GameBlock
    {
        public int MonsterId { get; set; }

        //Reference
        public virtual Monster Monster { get; set; }
    }
}
