import { Component, Input, OnInit } from '@angular/core';
import { IngameBlockDto } from 'src/app/_models/BlockDtos/ingameBlockDto';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-game-block',
  templateUrl: './game-block.component.html',
  styleUrls: ['./game-block.component.scss']
})
export class GameBlockComponent implements OnInit {
  @Input() blockComponentData: IngameBlockDto;
  imagePath = environment.blockImageUrl;
  cssMoveAnimation = "";
  cssDiscoverFade = "";

  constructor() { }

  ngOnInit(): void {
    this.blockComponentData.blockType.imageName = 'url('+this.imagePath+this.blockComponentData.blockType.imageName+')';
  }
}
