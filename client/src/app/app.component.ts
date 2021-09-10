import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { UserDto } from './_models/userDto';
import { AccountService } from './home/services/account/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'BoardGame';
  users : any;

  constructor(private http : HttpClient, private accountService: AccountService){}


  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser(){
    const user: UserDto = {userName : localStorage.getItem('user'), token : localStorage.getItem('userToken')}

    this.accountService.setCurrentUser(user);
  }

  getUsers(){
    this.http.get('https://localhost:44362/api/users').subscribe(response=>{
      this.users = response;
    }, error =>{
      console.log(error);
    })
  }
}
