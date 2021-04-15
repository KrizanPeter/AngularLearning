import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AccountService } from '../../services/account/account.service'

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  @Output() cancelEmitter = new EventEmitter();

  model:any = {};
  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
  }

  register():void{
    this.accountService.register(this.model).subscribe(response => {
      console.log(response);
    }, error =>{
      console.log(error);
    });
  }

  cancel():void{
    console.log("canceled");
    this.cancelEmitter.emit(false);
  }
}
