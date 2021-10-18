using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Domain.Models
{
    public class MonsterModel
    {
        public int MonsterId { get; set; }
        public int MonsterTypeId { get; set; }
        public string MonsterName { get; set; }
        public int Level { get; set; }
        public int Life { get; set; }
        public int DmgMin { get; set; }
        public int DmgMax { get; set; }
        public int Armor { get; set; }
        public MonsterTypeModel MonsterType { get; set; }
    }
}
