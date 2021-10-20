import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { tap } from "rxjs/operators";
import { BattleReportDto } from "src/app/_models/BattleReportDto/BattleReportDto";
import { environment } from "src/environments/environment";
import { BlockWindow } from "../components/game-board/game-board.component";

@Injectable({
    providedIn: 'root'
  })
  export class GameBoardService {

    baseUrl = environment.apiUrl;
  
    constructor(private http: HttpClient) { }
  
    getGameSessions(renderWindow: BlockWindow){
      return this.http.post(this.baseUrl+"coregame/loadgame", renderWindow).pipe(
        tap((response: any)=>{
          console.log(response);
        }));
      }

    leaveSession(){
      return this.http.get(this.baseUrl+"session/leavesession").pipe(
        tap((response: any)=>{
          console.log(response);
        }));
    }

    resolveConflictOnBlock(blockId: number) {
      return this.http.post<BattleReportDto>(this.baseUrl+"coregame/resolveconflict", blockId); 
    }
  }
  