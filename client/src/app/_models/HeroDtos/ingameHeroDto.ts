import { HeroType } from "../enums/enumsDtos";

export interface IngameHeroDto{
    heroId : number
    hlockId : number
    appUserId : number
    heroTypeId : number
    heroName : string
    heroType : HeroType 
    imagePath : string
    level : number
    lives : number
    livesCap : number
    dmgMin: number
    dmgMax : number
    armor: number
    experience : number
    experienceCap: number
    skillPoints: number
}