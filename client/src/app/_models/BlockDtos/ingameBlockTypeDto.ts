import { BlockCategory } from "../enums/enumsDtos";

export interface IngameBlockTypeDto{
     blockTypeId : number
     imageName : string  
     exitTop : Boolean  
     exitDown : Boolean  
     exitRight : Boolean  
     exitLeft : Boolean  
     blockCategory : BlockCategory  
}