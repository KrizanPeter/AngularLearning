import { Component, OnDestroy, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { AccountService } from 'src/app/home/services/account/account.service';
import { MessageDto } from 'src/app/_models/MessageDtos/MessageDto';
import { UserDto } from 'src/app/_models/userDto';
import { ChatService } from 'src/app/_services/chatservice/chat.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit, OnDestroy {
  messageContent = "";
  user: UserDto;
  messages: MessageDto[] = [];

  constructor(public chatService : ChatService, private accountService: AccountService) { 
    this.accountService.currentUsers$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnDestroy(): void {
    this.chatService.stopHubConnection();
  }

  ngOnInit(): void {
    this.chatService.createHubConnection(this.user);
  }

  sendMessage(){
    let message = {} as MessageDto;
    message.sender = this.user.userName;
    message.message = this.messageContent;
    this.chatService.sendMessage(message).then(()=>{
      console.log(message);
      this.messages.push(message);
    })
  }
}
