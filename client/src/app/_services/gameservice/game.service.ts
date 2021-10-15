import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject, interval, Observable, of } from 'rxjs';
import { IngameBlockDto } from 'src/app/_models/BlockDtos/ingameBlockDto';
import { UserDto } from 'src/app/_models/userDto';
import { environment } from 'src/environments/environment';
import { HttpClient } from "@angular/common/http";
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  hubUrl = environment.hubUrl;
  baseUrl = environment.apiUrl;
  private hubConnection: HubConnection;
  private blocksThread = new BehaviorSubject<IngameBlockDto[]>([]);
  blocksThread$ = this.blocksThread.asObservable();
  playerName$ : Observable<string>;
  turnCountdown: number;
  curSec: number = 0;

  constructor(private toastr: ToastrService, private http: HttpClient) { }

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
    this.hubConnection.on('EndTurnDetected', activePlayerModel => {
      console.log("new active player", activePlayerModel);
      this.toastr.success("It is " + activePlayerModel.playerName + "'s turn");
      this.turnCountdown = 100;
      this.startTimer(activePlayerModel.remainingSeconds);
      this.playerName$ = of(activePlayerModel.playerName);
    });
  }

  stopHubConnection() {
    this.hubConnection.stop().catch(error => console.log(error));
  }

  async moveHero(targetBlockDto: IngameBlockDto) {
    return this.hubConnection.invoke("MoveHero", targetBlockDto)
      .catch(error => console.log(error));
  }

  public initializeCurrentTurn(sessionId: number)
  {
    return this.http.get(this.baseUrl + "coregame/initializecurrentturn/?sessionId=" + sessionId).pipe(
      tap((response: any) => {
        console.log(response);
        //this.turnCountdown = 100;
        //this.startTimer(response.remainingSeconds);
        this.playerName$ = of(response.playerName);
      }));
  }

  private startTimer(seconds: number) {
    const time = seconds;
    const timer$ = interval(1000);

    const sub = timer$.subscribe((sec) => {
      this.turnCountdown = 100 - sec * 100 / seconds;
      this.curSec = sec;

      if (this.curSec === seconds) {
        sub.unsubscribe();
      }
    });
  }
}
