import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Body } from "@angular/http/src/body";
import { map } from "rxjs-compat/operator/map";
import { tap } from "rxjs/operators";
import { Connections } from "src/app/_conf/connections";
import { IngameBlockDto } from "src/app/_models/BlockDtos/ingameBlockDto";
import { IngameSessionDto } from "src/app/_models/SessionDtos/ingameSessionDto";
import { BlockWindow } from "../components/game-board/game-board.component";

@Injectable({
    providedIn: 'root'
  })
  export class GameBoardService {
     _con = new Connections()
  
    constructor(private http: HttpClient) { }
  
    getGameSessions(renderWindow: BlockWindow){
      return this.http.post(this._con.baseUrl+"coregame/loadgame", renderWindow).pipe(
        tap((response: any)=>{
          console.log(response);
        }));
      }

    leaveSession(){
      return this.http.get(this._con.baseUrl+"session/leavesession").pipe(
        tap((response: any)=>{
          console.log(response);
        }));
    }
  }
  