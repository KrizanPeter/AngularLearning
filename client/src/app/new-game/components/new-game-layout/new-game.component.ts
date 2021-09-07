import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { JoinToGameComponent } from '../join-to-game/join-to-game/join-to-game.component';

@Component({
  selector: 'app-new-game',
  templateUrl: './new-game.component.html',
  styleUrls: ['./new-game.component.scss']
})
export class NewGameComponent implements OnInit {

  @ViewChild(JoinToGameComponent) joinCmp : JoinToGameComponent;
  
  constructor() { }

  ngOnInit(): void {
  }

  refreshSession(event: boolean){
    this.joinCmp.refreshSessions();
  }

}
