import { Component, Input, OnInit } from '@angular/core';
import { IngameBlockDto } from 'src/app/_models/BlockDtos/ingameBlockDto';
import { BlockType } from 'src/app/_models/enums/enumsDtos';
import { GameService } from 'src/app/_services/gameservice/game.service';

@Component({
  selector: 'app-game-block',
  templateUrl: './game-block.component.html',
  styleUrls: ['./game-block.component.scss']
})
export class GameBlockComponent implements OnInit {
  @Input() blockComponentData: IngameBlockDto;

  constructor() { }

  ngOnInit(): void {
  }
  
}
