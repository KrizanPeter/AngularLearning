import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SessionService } from 'src/app/new-game/services/session-service.service';
import { SessionDto } from 'src/app/_models/sessionDto';

@Component({
  selector: 'app-create-game',
  templateUrl: './create-game.component.html',
  styleUrls: ['./create-game.component.scss']
})
export class CreateGameComponent implements OnInit {

  @Output() sessionEmitter = new EventEmitter<boolean>();
  model = {} as SessionDto;

  constructor(private sessionService: SessionService, private router:Router, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  createSession(){
    this.sessionService.createSession(this.model).subscribe(response=>{
      this.sessionEmitter.emit(true);
    }, error =>{
      console.log(error);
      this.toastr.error(error.error);
    });
  }
}