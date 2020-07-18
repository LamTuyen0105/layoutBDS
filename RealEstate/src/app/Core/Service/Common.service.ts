import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from 'src/environments/environment';

const API = environment.BaseURI;
@Injectable({
  providedIn: 'root'
})
export class CommonService {

  constructor(private http: HttpClient) { }

  getProvice() {
    return this.http.get(API + '/SampleDatum/Province');
  }
  getDistrict(provinceId) {
    return this.http.get(API + '/SampleDatum/District?ProvinceId=' + provinceId);
  }
  getWard(districtId) {
    return this.http.get(API + '/SampleDatum/Ward?DistrictId=' + districtId);
  }
  getDirection() {
    return this.http.get(API + '/SampleDatum/Direction');
  }
  getTypeOfProperty() {
    return this.http.get(API + '/SampleDatum/TypeOfProperty');
  }
  getTypeOfTransaction() {
    return this.http.get(API + '/SampleDatum/TypeOfTransaction');
  }
  getEvaluationStatus() {
    return this.http.get(API + '/SampleDatum/EvaluationStatus');
  }
  getLegalPaper() {
    return this.http.get(API + '/SampleDatum/LegalPaper');
  }
  getHotSellProperties() {
    return this.http.get(API + '/Properties/HotSellProperties');
  }
  getHotRentProperties() {
    return this.http.get(API + '/Properties/HotRentProperties');
  }
  getProperties(propertyId) {
    return this.http.get(API + '/Properties/' + propertyId);
  }
  getPropertyImage(propertyId) {
    return this.http.get(API + '/Properties/' + propertyId + '/images');
  }
  getPropertyImageNonDefault(propertyId) {
    return this.http.get(API + '/Properties/' + propertyId + '/imagenondefault');
  }
  getPropertyImageDefault(propertyId) {
    return this.http.get(API + '/Properties/' + propertyId + '/imagedefault');
  }
}
