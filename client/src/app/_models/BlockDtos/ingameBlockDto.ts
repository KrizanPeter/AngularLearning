import { IngameHeroDto } from "../HeroDtos/ingameHeroDto";
import { IngameBlockTypeDto } from "./ingameBlockTypeDto";

export interface IngameBlockDto{
    blockId : number
    sessionId : number
    monsterId : number
    blockPositionX: number
    blockPositionY : number
    blockOrder: number
    blockType : IngameBlockTypeDto
    heroes: IngameHeroDto[]
}