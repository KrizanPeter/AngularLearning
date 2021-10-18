import { IngameHeroDto } from "../HeroDtos/ingameHeroDto";
import { IngameMonsterDto } from "../MonsterDtos/IngameMonsterDto";
import { IngameBlockTypeDto } from "./ingameBlockTypeDto";

export interface IngameBlockDto{
    blockId : number
    sessionId : number
    monsterId : number
    blockPositionX: number
    blockPositionY : number
    blockOrder: number
    incomingMovement: string
    monster: IngameMonsterDto
    blockType : IngameBlockTypeDto
    heroes: IngameHeroDto[]
}