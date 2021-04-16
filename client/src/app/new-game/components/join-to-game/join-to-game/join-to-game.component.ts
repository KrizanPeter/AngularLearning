import { Component, OnInit } from '@angular/core';

export interface PeriodicElement {
  name: string;
  position: number;
  date: number;
  players: string;
}

const ELEMENT_DATA: PeriodicElement[] = [
  {position: 1, name: 'Partička hráčov', date: 1.0079, players: '4/5'},
  {position: 2, name: 'Testovacia partička', date: 4.0026, players: '2/5'},
  {position: 3, name: 'Lithium', date: 6.941, players: '3/5'},
  {position: 4, name: 'Beryllium', date: 9.0122, players: '4/5'},
  {position: 5, name: 'Boron', date: 10.811, players: '1/5'},
  {position: 6, name: 'Carbon', date: 12.0107, players: '1/5'},
];

@Component({
  selector: 'app-join-to-game',
  templateUrl: './join-to-game.component.html',
  styleUrls: ['./join-to-game.component.scss']
})

export class JoinToGameComponent implements OnInit {

  displayedColumns: string[] = ['position', 'name', 'date', 'players'];
  dataSource = ELEMENT_DATA;
  selectedRowIndex = 0;
  constructor() { }

  ngOnInit(): void {
  }

  getRecord(row :any){
    this.selectedRowIndex = row.position;
  }

}
