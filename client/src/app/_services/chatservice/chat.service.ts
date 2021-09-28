import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { ToastrService } from 'ngx-toastr';
import { async, BehaviorSubject, of } from 'rxjs';
import { MessageDto } from 'src/app/_models/MessageDtos/MessageDto';
import { UserDto } from 'src/app/_models/userDto';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  hubUrl = environment.hubUrl;
  private hubConnection: HubConnection;
  private messagesThread = new BehaviorSubject<MessageDto[]>([]);
  messageThread$ = this.messagesThread.asObservable();

  constructor(private toastr: ToastrService) { }

  createHubConnection(sessionId : number, user: UserDto){
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl + 'message?sessionId=' + sessionId, {
        accessTokenFactory: () => user.token
      })
      .withAutomaticReconnect()
      .build()

      this.hubConnection
      .start()
      .catch(error=>console.log(error));
      
     this.hubConnection.on('RecieveInstantMessage', messages=> {
       this.messagesThread.next(messages);
     })
  }

  stopHubConnection(){
    this.hubConnection.stop().catch(error=> console.log(error));
  }

  async sendMessage(messageDto : MessageDto){
    return this.hubConnection.invoke("SendMessage", messageDto)
    .catch(error=>console.log(error));
  }

}
