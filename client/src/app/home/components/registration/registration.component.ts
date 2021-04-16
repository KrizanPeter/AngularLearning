import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../../services/account/account.service'

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  @Output() cancelEmitter = new EventEmitter();

  model:any = {};
  constructor(private accountService: AccountService, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  register():void{
    this.accountService.register(this.model).subscribe(response => {
    }, error =>{
      console.log(error.error.title);
      this.toastr.error(error.error.title);
    });
  }

  cancel():void{
    console.log("canceled");
    this.cancelEmitter.emit(false);
  }
}
