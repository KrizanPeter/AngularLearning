using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.Domain.Entities
{
    public class Monster
    {
        public int MonsterId { get; set; }
        public int MonsterTypeId { get; set; }
        public string MonsterName { get; set; }

        //References
        public virtual MonsterType MonsterType { get; set; }

    }
}
