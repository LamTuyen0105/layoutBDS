import { Injectable } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

const API = environment.BaseURI;
@Injectable({
  providedIn: 'root'
})
export class MainService {

  constructor(private fb: FormBuilder, private http: HttpClient) { }

  formModels = this.fb.group({
    SDT: ['', Validators.required],
    password: this.fb.group({
      MatKhau: ['', [Validators.required, Validators.minLength(4)]],
      MatKhauNhapLai: ['', Validators.required],
    },
      {
        validators: this.comparepassword
      }),
    HoTen: ['', Validators.required],
    Mail: ['', [Validators.email, Validators.required]],
    DiaChi: [''],
    GioiTinh: ['']

  });

  comparepassword(fb: FormGroup) {
    let passwordnhaplai = fb.get('MatKhauNhapLai');
    if (passwordnhaplai.errors == null || 'matchpassword' in passwordnhaplai.errors) {
      if (fb.get('MatKhau').value != passwordnhaplai.value) {
        passwordnhaplai.setErrors({ matchpassword: true });
      }
      else {
        passwordnhaplai.setErrors(null);
      }
    }
  }

  regist() {
    var body = {
      SDT: this.formModels.value.SDT,
      MatKhau: this.formModels.value.password.MatKhau,
      HoTen: this.formModels.value.HoTen,
      Mail: this.formModels.value.Mail,
      DiaChi: this.formModels.value.DiaChi,
      GioiTinh: this.formModels.value.GioiTinh
    }
    return this.http.post(API + '/NguoiDung', body);
  }

  login(formDatas) {
    return this.http.post(API + '/Users/authenticate', formDatas);
  }

  getProperty() {
    return this.http.get(API + '/PropertyNewsManagers');
  }

getUsers(){
  return this.http.get(API + '/Users'); //lấy lên user
}

  deletteProperties(propertyId){
    let formData;
    return this.http.patch(API + '/PropertyNewsManagers/' + propertyId + '/true', formData);
  }

  getProperties(propertyId) {
    return this.http.get(API + '/Properties/' + propertyId);
  }

  getPropertyImageNonDefault(propertyId) {
    return this.http.get(API + '/Properties/' + propertyId + '/imagenondefault');
  }

  getPropertyImageDefault(propertyId) {
    return this.http.get(API + '/Properties/' + propertyId + '/imagedefault');
  }
}