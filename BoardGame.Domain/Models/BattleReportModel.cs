using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Domain.Models
{
    public class BattleReportModel
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
        public BattleReportModel() { }
        public BattleReportModel(string battleResult, string imgAttacker, string imgDefender, string attackerName, string defenderName, int attackerHealthCap, int attackerHealthCurrent, int defenderHealthCap, int defenderHealthCurrent, int attackerDmgMin, int defenderDmgMin, int attackerDmgMax, int defenderDmgMax, int attackerArmor, int defenderArmor, int recievedExperience)
        {
            BattleResult = battleResult;
            ImgAttacker = imgAttacker;
            ImgDefender = imgDefender;
            AttackerName = attackerName;
            DefenderName = defenderName;
            AttackerHealthCap = attackerHealthCap;
            AttackerHealthCurrent = attackerHealthCurrent;
            DefenderHealthCap = defenderHealthCap;
            DefenderHealthCurrent = defenderHealthCurrent;
            AttackerDmgMin = attackerDmgMin;
            DefenderDmgMin = defenderDmgMin;
            AttackerDmgMax = attackerDmgMax;
            DefenderDmgMax = defenderDmgMax;
            AttackerArmor = attackerArmor;
            DefenderArmor = defenderArmor;
            RecievedExperience = recievedExperience;
        }
    }
}
