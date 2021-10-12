import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { tap } from 'rxjs/operators';
import { IngameSessionDto } from 'src/app/_models/SessionDtos/ingameSessionDto';
import { SessionDto } from 'src/app/_models/SessionDtos/sessionDto';
import { UserDto } from 'src/app/_models/userDto';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SessionService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  createSession(model: SessionDto){
    return this.http.post(this.baseUrl+"session", model).pipe(
      tap((response: SessionDto) =>{
        console.log(response);
      })
    )
  }

  getSessions(){
    return this.http.get(this.baseUrl+"session").pipe(
      tap((response: IngameSessionDto)=>{
      })
    )
  }

  joinToSession(sessionId, user:UserDto){
    let joinToSession = {sessionId : sessionId, userName : user.userName}
    return this.http.post(this.baseUrl+"session/join", joinToSession).pipe(
      tap((response: any)=>{
        console.log(response);
      })
    )
  }
}
