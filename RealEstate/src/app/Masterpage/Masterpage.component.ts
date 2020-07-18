import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
// import * as $ from "jquery";

import { UserServiceService } from 'src/app/Core/UserService/UserService.service';
import { CommonService } from 'src/app/Core/Service/Common.service';


@Component({
  selector: 'app-Masterpage',
  templateUrl: './Masterpage.component.html',
  styleUrls: ['./Masterpage.component.css']
})
export class MasterpageComponent implements OnInit {

  Province = [];
  District = [];
  Wards = [];
  formModels = {
    UserName: '',
    Password: ''
  }
  
  submitted= false;

  constructor(private router: Router,public service: UserServiceService, 
    private services: CommonService, 
    ) { }

  ngOnInit() {
    this.services.getProvice().subscribe(res => this.Province = res as []);
    this.service.formModel.reset();
    if (localStorage.getItem('token') == null)
    {
      this.submitted = false;
    }
  }
  

  onSubmit() {
    this.service.register().subscribe(
      data => {
          this.service.formModel.reset();
      },
      err => {
        console.log(err);
      }
    );
  }

  onSubmits(forms: NgForm) {
    this.service.login(forms.value).subscribe(
      (res: any) => {
        localStorage.setItem('token', res.token);
        this.formModels.UserName = '';
        this.formModels.Password = '';
        $('#Login').modal('toggle');
        this.submitted = true;
        console.log(res);
      },
      err => {
        if (err.status == 400)
          console.log(err);
        else
          console.log(err);
      }
    );
  }

  OnClick(){
    this.router.navigateByUrl('/DangTin');
  }

  onLogout() {
    localStorage.removeItem('token');
    this.submitted = false;
    this.router.navigate(['/TrangChu']);
  }

  onProfile(){
    this.router.navigate(['/ThongTin']);
  }

  onMyPost(){
    this.router.navigate(['/QuanLyTin']);
  }
  
  GetProvinceId (event) {
    let id = event.target.value;
    return this.services.getDistrict(id).subscribe(res => this.District = res as []);
  }

  GetDistrictId (event) {
    let id = event.target.value;
    return this.services.getWard(id).subscribe(res => this.Wards = res as []);
  }
}