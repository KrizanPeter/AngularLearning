import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { tap } from "rxjs/operators";
import { Connections } from "src/app/_conf/connections";

@Injectable({
    providedIn: 'root'
  })
  export class GameBoardService {
     _con = new Connections()
  
    constructor(private http: HttpClient) { }
  
    getGameSessions(){
      return this.http.get(this._con.baseUrl+"coregame/loadgame").pipe(
        tap((response: any)=>{
          console.log(response);
        })
      )
    }
  }
  