import { BlockDirection, BlockType } from "../enums/enumsDtos";
import { IngameHeroDto } from "../HeroDtos/ingameHeroDto";

export interface IngameBlockDto{
    blockId : number
    sessionId : number
    monsterId : number
    blockPositionX: number
    blockPositionY : number
    blockOrder: number
    blockType : BlockType
    blockDirection : BlockDirection
    heroes: IngameHeroDto[]
}