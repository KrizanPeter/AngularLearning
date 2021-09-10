import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { GameBoardService } from '../../services/game-board.service';

@Component({
  selector: 'app-game-board',
  templateUrl: './game-board.component.html',
  styleUrls: ['./game-board.component.scss']
})
export class GameBoardComponent implements OnInit {

  constructor(private gameBoardService : GameBoardService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadGame();
  }

  loadGame(){
    this.gameBoardService.getGameSessions().subscribe(response=>{
      console.log(response);
    }, error =>{
      console.log(error);
      this.toastr.error(error.error);
    });

    
  }
}
