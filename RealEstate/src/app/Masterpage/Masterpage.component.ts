import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
// import * as $ from "jquery";
import { SocialAuthService } from "angularx-social-login";
import { SocialUser } from "angularx-social-login";
import { FacebookLoginProvider, GoogleLoginProvider } from "angularx-social-login";

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
  submitted = true;
  user: SocialUser;
  loggedIn: boolean;
  formemail = {
    email: ''
  }
  checkemail = [];
  emailerror: boolean = false;

  constructor(private router: Router, public service: UserServiceService,
    private services: CommonService, private authService: SocialAuthService
  ) { }

  ngOnInit() {
    this.services.getProvice().subscribe(res => this.Province = res as []);
    this.service.formModel.reset();
    this.authService.authState.subscribe((user) => {
      this.user = user;
      console.log(this.user)
      var body = {
        UserName: this.user.email,
        FullName: this.user.lastName + ' ' + this.user.firstName,
        Provider: this.user.provider,
        SocialId: this.user.id
      };
      this.service.loginsocial(body).subscribe(
        (res: any) => {
          localStorage.setItem('token', res.token);
          console.log(res);
        },
        err => {
          if (err.status == 400) {
            console.log(err);
          }
          else
            console.log(err);
          this.service.registersocial(body).subscribe(
            data => {
              this.service.formModel.reset();
            },
            err => {
              console.log(err);
            }
          );
          this.service.loginsocial(body).subscribe(
            (res: any) => {
              localStorage.setItem('token', res.token);
              console.log(res);
            },
            err => {
              if (err.status == 400) {
                console.log(err);
              }
              else
                console.log(err);
            }
          );
        }
      );
      if (this.user.authToken != '') {
        $('#Login').modal('toggle');
        this.router.navigateByUrl('/TrangChu');
      }
      this.submitted = true;
      this.loggedIn = (user != null);
    });
    if (localStorage.getItem('token') == null) {
      this.submitted = false;
    }
  }

  signInWithGoogle(): void {
    this.authService.signIn(GoogleLoginProvider.PROVIDER_ID);
  }

  signInWithFB(): void {
    this.authService.signIn(FacebookLoginProvider.PROVIDER_ID);
  }

  signOut(): void {
    this.authService.signOut();
  }

  onSubmit() {
    this.service.register().subscribe(
      data => {
        this.service.formModel.reset();
        $('#Signup').modal('hide');
        $('#Login').modal('show');
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
        this.router.navigateByUrl('/TrangChu');
        console.log(res);
      },
      err => {
        if (err.status == 400) {
          console.log(err);
        }
        else
          console.log(err);
      }
    );
  }

  OnClick() {
    this.router.navigateByUrl('/DangTin');
  }

  onLogout() {
    this.signOut();
    localStorage.removeItem('token');
    this.router.navigate(['/TrangChu']);
    this.submitted = false;
  }

  onProfile() {
    this.router.navigate(['/ThongTin']);
  }

  onMyPost() {
    this.router.navigate(['/QuanLyTin']);
  }

  GetProvinceId(event) {
    let id = event.target.value;
    return this.services.getDistrict(id).subscribe(res => this.District = res as []);
  }

  GetDistrictId(event) {
    let id = event.target.value;
    return this.services.getWard(id).subscribe(res => this.Wards = res as []);
  }

  VerifyEmail() {
    this.service.VerifyEmail(this.formemail.email).subscribe((res: any) => {      
      if(res.status == 0)
      {
        this.emailerror = true;
      }
      else{
        this.emailerror = false;
      }
      
      console.log(res.status)
    });
    console.log(this.emailerror)
  }
}