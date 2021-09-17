import { BlockDirection, BlockType } from "../enums/enumsDtos";

export interface IngameBlockDto{
    blockId : number
    sessionId : number
    monsterId : number
    blockPositionX: number
    blockPositionY : number
    blockOrder: number
    blockType : BlockType
    blockDirection : BlockDirection
}