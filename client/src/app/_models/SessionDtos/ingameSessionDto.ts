import { IngameBlockDto } from "../BlockDtos/ingameBlockDto";
import { PlanSize, SessionType } from "../enums/enumsDtos";

export interface IngameSessionDto{
    sessionId : number
    SessionName : string 
    sessionPassword : string 
    sessionType : SessionType
    slanSize : PlanSize
    senterBlockPosition : number
    blocksShape: IngameBlockDto[][]

}