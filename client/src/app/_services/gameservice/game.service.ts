import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject } from 'rxjs';
import { IngameBlockDto } from 'src/app/_models/BlockDtos/ingameBlockDto';
import { UserDto } from 'src/app/_models/userDto';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  hubUrl = environment.hubUrl;
  private hubConnection: HubConnection;
  private blocksThread = new BehaviorSubject<IngameBlockDto[]>([]);
  blocksThread$ = this.blocksThread.asObservable();

  constructor(private toastr: ToastrService) { }

  createHubConnection(sessionId: number, user: UserDto) {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl + 'board?sessionId=' + sessionId, {
        accessTokenFactory: () => user.token
      })
      .withAutomaticReconnect()
      .build()

    this.hubConnection
      .start()
      .catch(error => console.log(error));

    this.hubConnection.on('MovementDetected', blocks => {
      console.log("hero moved service", blocks);
      this.blocksThread.next(blocks);
    });
    this.hubConnection.on('MovementFailed', err => {
      console.log("error", err);
      err.forEach(element => {
        this.toastr.error(element.description);
      });
    });
    this.hubConnection.on('EndTurnDetected', userName => {
      console.log("new active player", userName);
      this.toastr.success("It is " + userName + "'s turn");
    });
  }

  stopHubConnection() {
    this.hubConnection.stop().catch(error => console.log(error));
  }

  async moveHero(targetBlockDto: IngameBlockDto) {
    return this.hubConnection.invoke("MoveHero", targetBlockDto)
      .catch(error => console.log(error));
  }
}
