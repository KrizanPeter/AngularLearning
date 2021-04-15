import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../../_models/user';
import { AccountService } from '../../home/services/account/account.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  currentUser$: Observable<User>;
  model: any = {
    username: "",
    password: "",
  }
  constructor( private accountService: AccountService) { }

  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUsers$;
    console.log(this.currentUser$);
  }

  login(){
    this.accountService.login(this.model).subscribe(response => {
      console.log(response);
    }, error =>{
      console.log(error);
    });
  }

  logout(){
    this.accountService.logout();
  }
}
