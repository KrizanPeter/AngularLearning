import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, tap } from 'rxjs/operators';
import { Connections } from 'src/app/_conf/connections';
import { IngameSessionDto } from 'src/app/_models/SessionDtos/ingameSessionDto';
import { SessionDto } from 'src/app/_models/SessionDtos/sessionDto';
import { UserDto } from 'src/app/_models/userDto';

@Injectable({
  providedIn: 'root'
})
export class SessionService {
   _con = new Connections()

  constructor(private http: HttpClient) { }

  createSession(model: SessionDto){
    return this.http.post(this._con.baseUrl+"session", model).pipe(
      tap((response: SessionDto) =>{
        console.log(response);
      })
    )
  }

  getSessions(){
    return this.http.get(this._con.baseUrl+"session").pipe(
      tap((response: IngameSessionDto)=>{
      })
    )
  }

  joinToSession(sessionId, user:UserDto){
    let joinToSession = {sessionId : sessionId, userName : user.userName}
    return this.http.post(this._con.baseUrl+"session/join", joinToSession).pipe(
      tap((response: any)=>{
        console.log(response);
      })
    )
  }
}
