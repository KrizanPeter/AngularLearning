import { Component, Input, OnInit } from '@angular/core';
import { IngameBlockDto } from 'src/app/_models/BlockDtos/ingameBlockDto';
import { BlockType } from 'src/app/_models/enums/enumsDtos';

@Component({
  selector: 'app-game-block',
  templateUrl: './game-block.component.html',
  styleUrls: ['./game-block.component.scss']
})
export class GameBlockComponent implements OnInit {
  @Input() blockComponentData: IngameBlockDto;
  blockBackgroundClass = "";
  constructor() { }

  ngOnInit(): void {
    console.log(this.blockComponentData);
    this.imageBlockSetup();
  }

  imageBlockSetup(){
    console.log('Invoked block setup')
    if(this.blockComponentData.blockType.valueOf() === BlockType.Hidden.valueOf())
    {
      this.blockBackgroundClass = 'hidden-block';
      console.log(this.blockBackgroundClass);
    }
    else if(this.blockComponentData.blockType.valueOf() === BlockType.Room.valueOf())
    {
      console.log("STREDDDDD<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
      this.blockBackgroundClass = 'room-block';
      console.log(this.blockComponentData.heroes[0].imagePath)
    }
    else if(this.blockComponentData.blockType.valueOf() === BlockType.Hidden.valueOf())
    {
      this.blockBackgroundClass = 'hidden-block';
    }
  }

}
