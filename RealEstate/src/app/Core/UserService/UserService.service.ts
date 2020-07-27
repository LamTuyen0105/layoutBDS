import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient, HttpHeaders } from "@angular/common/http";

import { environment } from 'src/environments/environment';

const API = environment.BaseURI;

@Injectable({
  providedIn: 'root'
})
export class UserServiceService {

constructor(private fb: FormBuilder, private http: HttpClient) { }

  formModel = this.fb.group({
    UserName: ['', Validators.email],
    Passwords: this.fb.group({
      Password: ['', [Validators.required, Validators.minLength(6)]],
      ConfirmPassword: ['', Validators.required]
    }, { validator: this.comparePasswords }),    
    FullName: ['', Validators.required],
    Gender: ['', Validators.required],
    WardId: [0, Validators.required],
    IdentityNumber:['', Validators.required]
  });

  comparePasswords(fb: FormGroup) {
    let confirmPswrdCtrl = fb.get('ConfirmPassword');
    if (confirmPswrdCtrl.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors) {
      if (fb.get('Password').value != confirmPswrdCtrl.value)
        confirmPswrdCtrl.setErrors({ passwordMismatch: true });
      else
        confirmPswrdCtrl.setErrors(null);
    }
  }

  register() {
    var body = {
      UserName: this.formModel.value.UserName,
      FullName: this.formModel.value.FullName,
      Password: this.formModel.value.Passwords.Password,
      ConfirmPassword: this.formModel.value.Passwords.ConfirmPassword,
      Gender: this.formModel.value.Gender,
      WardId: Number(this.formModel.value.WardId),
      IdentityNumber: this.formModel.value.IdentityNumber
    };
    return this.http.post(API + '/Users/register', body);
  }

  login(formData) {
    return this.http.post(API + '/Users/authenticate', formData);
  }

  getUserProfile() {
    return this.http.get(API + '/Users');
  }

  getPropertyUser() {
    return this.http.get(API + '/Properties/PropertyByUser');
  }

  postPropertyUser(form) {
    return this.http.post(API + '/Users', form);
  }

  registersocial(formdata) {    
    return this.http.post(API + '/Users/registersocial', formdata);
  }

  loginsocial(formData) {
    return this.http.post(API + '/Users/authenticatesocial', formData);
  }

  VerifyEmail(email){
    return this.http.get('https://app.verify-email.org/api/v1/JrnkBFfmE9gCGzRL1FO3cWhXJSUNQkBiVC3almzIRVAraY1ZfG/verify/' + email);
  }
}
