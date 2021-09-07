import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { UserDto } from 'src/app/_models/userDto';

@Injectable({
  providedIn: 'root'
})

export class AccountService {
  baseUrl = 'https://localhost:44362/api/';
  private currentUserSource = new ReplaySubject<UserDto>(1)
  currentUsers$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }

  register(model:any){
    return this.http.post(this.baseUrl+'account/register', model).pipe(
      map((response:UserDto) =>{
        const user = response;
        if(user)
        {
          localStorage.setItem('user', user.userName);
          localStorage.setItem('userToken', user.token);
          this.currentUserSource.next(user);
        }
      })
    )
  }

  login(model: any){
    return this.http.post(this.baseUrl + 'account/login', model).pipe(
      map((response: UserDto) => {
        const user = response;
        if(user){
          localStorage.setItem('user', user.userName);
          localStorage.setItem('userToken', user.token);
          this.currentUserSource.next(user);
        }
      })
    )
  }

  setCurrentUser(user: UserDto){
    this.currentUserSource.next(user);
  }

  logout(){
    localStorage.removeItem('user');
    localStorage.removeItem('userToken');
    this.currentUserSource.next(null);
  }
}
