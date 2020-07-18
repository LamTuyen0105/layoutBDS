import { Component, OnInit } from '@angular/core';

import {CommonService } from 'src/app/Core/Service/Common.service';

@Component({
  selector: 'app-Fillter',
  templateUrl: './Fillter.component.html',
  styleUrls: ['./Fillter.component.css']
})
export class FillterComponent implements OnInit {

  Province = [];
  District = [];
  Ward = [];
  Direction = [];
  TypeOfProperty = [];
  EvaluationStatus = [];
  
  constructor(private service: CommonService) { }

  ngOnInit() {
    this.service.getProvice().subscribe(res => this.Province = res as []);
    this.service.getDirection().subscribe(res => this.Direction = res as []);
    this.service.getTypeOfProperty().subscribe(res => this.TypeOfProperty = res as []);
    this.service.getEvaluationStatus().subscribe(res => this.EvaluationStatus = res as []);
  }

  GetProvinceId (event) {
    let id = event.target.value;
    return this.service.getDistrict(id).subscribe(res => this.District = res as []);
  }

  GetDistrictId (event) {
    let id = event.target.value;
    if (id != "")
      return this.service.getWard(id).subscribe(res => this.Ward = res as []);
  }
}