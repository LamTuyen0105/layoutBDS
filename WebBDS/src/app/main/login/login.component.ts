import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MainService } from 'src/app/share/main.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: []
})
export class LoginComponent implements OnInit {

  formModel = {
    UserName: '',
    Password: ''
  }
  constructor(private service: MainService, private router: Router, private toastr: ToastrService) { }

  ngOnInit() {
  }
  onSubmit(form:NgForm){
    this.service.login(form.value).subscribe(
      (res:any) => {
        localStorage.setItem('token', res.token);
        this.formModel.UserName ='';
        this.formModel.Password= '';
        this.router.navigateByUrl('/admin/detailhome');
      },
      err => {
        if (err.status == 400)
          console.log(err);
        else
          console.log(err);
      }
      );
    }
 }

