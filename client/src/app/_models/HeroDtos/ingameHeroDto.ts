import { HeroType } from "../enums/enumsDtos";

export interface IngameHeroDto{
    heroId : number
    blockId : number
    appUserId : number
    heroTypeId: number
    heroName: string
    heroType : HeroType
    imagePath: string
    lives : number
}