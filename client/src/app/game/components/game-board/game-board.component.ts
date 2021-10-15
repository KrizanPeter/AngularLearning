import { Component, OnDestroy, OnInit, QueryList, ViewChildren } from '@angular/core';
import { Router } from '@angular/router';
import { faArrowUp, faArrowDown, faArrowLeft, faArrowRight } from '@fortawesome/free-solid-svg-icons';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { AccountService } from 'src/app/home/services/account/account.service';
import { IngameBlockDto } from 'src/app/_models/BlockDtos/ingameBlockDto';
import { IngameSessionDto } from 'src/app/_models/SessionDtos/ingameSessionDto';
import { UserDto } from 'src/app/_models/userDto';
import { GameService } from 'src/app/_services/gameservice/game.service';
import { GameBoardService } from '../../services/game-board.service';
import { GameBlockComponent } from '../game-block/game-block.component';

@Component({
  selector: 'app-game-board',
  templateUrl: './game-board.component.html',
  styleUrls: ['./game-board.component.scss']
})
export class GameBoardComponent implements OnInit, OnDestroy {
  arrowUp = faArrowUp;
  arrowDown = faArrowDown;
  arrowLeft = faArrowLeft;
  arrowRight = faArrowRight;
  user: UserDto;
  @ViewChildren(GameBlockComponent) boardBlockViewChildren: QueryList<GameBlockComponent>;

  sessionData : IngameSessionDto;
  renderWindow: BlockWindow  = {
    startX : 7,
    startY: 9,
    endX: 15,
    endY: 13
  }
  
  constructor(private gameBoardService : GameBoardService,
    private toastr: ToastrService,
    private router: Router,
    public gameService: GameService,
    private accountService: AccountService) {
      this.accountService.currentUsers$.pipe(take(1)).subscribe(user => this.user = user);
    }

  ngOnInit(): void {
    this.loadGame(true);
    this.gameService.blocksThread$.subscribe(x => {
      for(let b of x)
      {
        let blockToRedraw = this.boardBlockViewChildren.find(f => f.blockComponentData.blockId == b.blockId);
        if (blockToRedraw) {
          blockToRedraw.blockComponentData = b;
          console.log("Incoming move", blockToRedraw.blockComponentData.incomingMovement);
          if (blockToRedraw.blockComponentData.incomingMovement) {
            blockToRedraw.cssMoveAnimation = "hero-move-animation-" + blockToRedraw.blockComponentData.incomingMovement;
            blockToRedraw.cssDiscoverFade = "discover-fade";
          }
          blockToRedraw.ngOnInit();
        }
      }
    })
  }

  ngOnDestroy(): void {
    this.gameService.stopHubConnection();
  }

  loadGame(isInit: boolean = false){
    this.gameBoardService.getGameSessions(this.renderWindow).subscribe(response=>{
      this.sessionData = response;
      if(isInit)
      {
        this.gameService.createHubConnection(this.sessionData.sessionId, this.user);
      }
      //console.log(this.sessionData);
    }, error =>{
      this.toastr.error(error.error);
    });
  }

  moveUp(){

    this.renderWindow.startY--;
    this.renderWindow.endY--;

    this.loadGame();
    console.log(this.renderWindow);
  }
  moveDown(){

    this.renderWindow.startY++;
    this.renderWindow.endY++;
    this.loadGame();
    console.log(this.renderWindow);
  }
  moveLeft(){
    console.log("moveleft");

    this.renderWindow.startX--;
    this.renderWindow.endX--;

    this.loadGame();
    console.log(this.renderWindow);
  }
  moveRight(){
    console.log("moveRight");

    this.renderWindow.startX++;
    this.renderWindow.endX++;

    this.loadGame();
    console.log(this.renderWindow);
  }

  leaveSession(){
    this.gameBoardService.leaveSession().subscribe(()=>{
      this.router.navigateByUrl('/game');
    }, error =>{
      this.toastr.error(error.error);
    });
  }

  moveHero(block: IngameBlockDto)
  {
    this.gameService.moveHero(block).then(() => {
      console.log("hero moved");
    })
  }
}

export interface BlockWindow{
  startX:number;
  endX:number;
  startY:number;
  endY:number;
}
