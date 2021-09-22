import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs/internal/Observable';
import { AccountService } from 'src/app/home/services/account/account.service';
import { SessionService } from 'src/app/new-game/services/session-service.service';
import { UserDto } from 'src/app/_models/userDto';

@Component({
  selector: 'app-join-to-game',
  templateUrl: './join-to-game.component.html',
  styleUrls: ['./join-to-game.component.scss']
})

export class JoinToGameComponent implements OnInit {
  
  sessions$ = {};
  displayedColumns: string[] = ['gameSessionId', 'sessionName'];
  selectedRowIndex = 0;
  userToJoin: UserDto;
  constructor(private sessionService: SessionService, private router: Router, private accountService : AccountService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.sessions$ = this.sessionService.getSessions();
  }

  refreshSessions(): void {
    this.ngOnInit();
  }

  getRecord(row :any){
    this.selectedRowIndex = row.sessionId;
    console.log(this.selectedRowIndex);
    this.accountService.currentUsers$.subscribe(res=>
      this.userToJoin = res);
  }

  joinToSession():void{

    this.sessionService.joinToSession(this.selectedRowIndex, this.userToJoin).subscribe(response=>{
      this.router.navigateByUrl('/pickhero');
    }, error =>{
      console.log(error);
      this.toastr.error(error.error);
    });
  }

}
