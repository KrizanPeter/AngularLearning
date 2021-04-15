import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../../_models/user';
import { AccountService } from '../../home/services/account/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  currentUser$: Observable<User>
  model: any = {
    username: "",
    password: "",
  }
  constructor( private accountService: AccountService, private router:Router) { }

  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUsers$;
    console.log(this.currentUser$);
  }

  login(){
    this.accountService.login(this.model).subscribe(response => {
      this.router.navigateByUrl('/game');
      console.log(response);
    }, error =>{
      console.log(error);
    });
  }

  logout(){
    this.accountService.logout();
  }
}
