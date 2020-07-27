import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

import { HttpClient } from "@angular/common/http";

const API = environment.BaseURL;
@Injectable({
  providedIn: 'root'
})
export class ElasticSearchService {

constructor(private http: HttpClient) { }

ElasticSearch(formDatas) {
  console.log(formDatas)
  return this.http.post(API, formDatas);
}
}
