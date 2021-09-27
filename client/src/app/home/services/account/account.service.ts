import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { UserDto } from 'src/app/_models/userDto';
import { ActivityService } from 'src/app/_services/activity.service';
import { ChatService } from 'src/app/_services/chatservice/chat.service';

@Injectable({
  providedIn: 'root'
})

export class AccountService {
  baseUrl = 'https://localhost:44362/api/';
  private currentUserSource = new ReplaySubject<UserDto>(1)
  currentUsers$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router, private activityService: ActivityService, private chatService: ChatService) { }

  register(model:any){
    return this.http.post(this.baseUrl+'account/register', model).pipe(
      map((response:UserDto) =>{
        const user = response;
        if(user)
        {
          localStorage.setItem('user', user.userName);
          localStorage.setItem('userToken', user.token);
          this.currentUserSource.next(user);
          this.activityService.createHubConnection(user);
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
          this.activityService.createHubConnection(user);
          this.chatService.createHubConnection(user);

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
    this.router.navigateByUrl('/home');
    this.currentUserSource.next(null);
    this.activityService.stopHubConnection();
    this.chatService.stopHubConnection();
  }
}
