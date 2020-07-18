import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient, HttpHeaders } from "@angular/common/http";

import { environment } from 'src/environments/environment';

const API = environment.BaseURI;

@Injectable({
  providedIn: 'root'
})
export class PostServiceService {

constructor(private fb: FormBuilder, private http: HttpClient) { }

  getPutProperties(propertyId) {
    return this.http.get(API + '/Properties/GetForPut/' + propertyId);
  }

  postProperty(formDatas) {
    console.log(formDatas)
    return this.http.post(API + '/Properties', formDatas);
  }

  putProperties(propertyId, formData) {
    return this.http.put(API + '/Properties/' + propertyId, formData);
  }

  deletteProperties(propertyId){
    return this.http.delete(API + '/Properties/' + propertyId);
  }
}
