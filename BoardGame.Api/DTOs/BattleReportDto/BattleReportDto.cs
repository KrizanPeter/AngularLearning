using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.Api.DTOs.BattleReportDto
{
    public class BattleReportDto
    {
        public string BattleResult { get; set; }
        public string ImgAttacker { get; set; }
        public string ImgDefender { get; set; }
        public string AttackerName { get; set; }
        public string DefenderName { get; set; }
        public int AttackerHealthCap { get; set; }
        public int AttackerHealthCurrent { get; set; }

        public int DefenderHealthCap { get; set; }
        public int DefenderHealthCurrent { get; set; }
        public int AttackerDmgMin { get; set; }
        public int DefenderDmgMin { get; set; }
        public int AttackerDmgMax { get; set; }
        public int DefenderDmgMax { get; set; }
        public int AttackerArmor { get; set; }
        public int DefenderArmor { get; set; }
        public int RecievedExperience { get; set; }
    }
}
