import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-create-game',
  templateUrl: './create-game.component.html',
  styleUrls: ['./create-game.component.scss']
})
export class CreateGameComponent implements OnInit {

  model: any;
  constructor() { }

  ngOnInit(): void {
  }

  createSession(){
    console.log("just create ");
  }
}
