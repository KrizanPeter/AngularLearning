import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs/internal/Observable';
import { AccountService } from 'src/app/home/services/account/account.service';
import { SessionService } from 'src/app/new-game/services/session-service.service';
import { UserDto } from 'src/app/_models/userDto';
import { ChatService } from 'src/app/_services/chatservice/chat.service';
import { GameService } from 'src/app/_services/gameservice/game.service';

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
  
  constructor(private sessionService: SessionService,
    private router: Router,
    private accountService : AccountService,
    private toastr: ToastrService,
    private chatService : ChatService)
  {}

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
      this.chatService.createHubConnection(this.selectedRowIndex, this.userToJoin);
      
      if(response === true){
        console.log("pick");
        this.router.navigateByUrl('/pickhero');
      }
      else if(response === false){
        this.router.navigateByUrl('/ingame');
        console.log("game");
      }
    }, error =>{
      console.log(error);
      this.toastr.error(error.error);
    });
  }

}
