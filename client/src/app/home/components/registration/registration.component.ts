import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
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
  constructor(private accountService: AccountService, private toastr: ToastrService, private router: Router) { }

  ngOnInit(): void {
  }

  register():void{
    this.accountService.register(this.model).subscribe(response => {
      this.router.navigateByUrl('/game');
    }, error =>{
      console.log(error);
      if(error.error){
        this.toastr.error(error.error.title);
        if(error.error.errors.Password)
        {
          error.error.errors.Password.forEach(error => {
            this.toastr.error(error);
          });
        }
      }
    });
  }

  cancel():void{
    console.log("canceled");
    this.cancelEmitter.emit(false);
  }
}
