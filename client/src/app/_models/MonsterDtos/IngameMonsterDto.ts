import { IngameMonsterTypeDto } from "./IngameMonsterType";

        
export interface IngameMonsterDto{
        monsterId : number
        monsterTypeId : number
        monsterName : string
        level: number
        life: number
        dmgMin : number
        dmgMax: number
        armor : number
        monsterType : IngameMonsterTypeDto
    }
