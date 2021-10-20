export interface BattleReportDto{
          battleResult : string
          imgAttacker : string 
          imgDefender : string 
          attackerName : string 
          defenderName : string 
          attackerHealthCap :number
          attackerHealthCurrent :number
          defenderHealthCap :number
          defenderHealthCurrent :number
          attackerDmgMin :number
          defenderDmgMin :number
          attackerDmgMax :number
          defenderDmgMax :number
          attackerArmor :number
          defenderArmor :number
          recievedExperience :number
}