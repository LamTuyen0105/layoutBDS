import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
// import * as $ from "jquery";

import { CommonService } from 'src/app/Core/Service/Common.service';
import { Console } from 'console';

@Component({
  selector: 'app-Home',
  templateUrl: './Home.component.html',
  styleUrls: ['./Home.component.css']
})
export class HomeComponent implements OnInit {

  Province = [];
  District = [];
  Ward = [];
  Direction = [];
  TypeOfProperty = [];
  EvaluationStatus = [];
  HotSellProperties = [];
  HotRentProperties = [];
  formModels = {
    Find: '',
    TypeOfProperty: 0,
    Price: 0,
    Area: 0,
    Direction: 0,
    RoomNumber: 0,
    EvaluationStatus: 0,
    Province: 0,
    District: 0,
    Ward: 0,
  };
  find: string;

  constructor(private router: Router, private service: CommonService) { }

  ngOnInit() {
    this.service.getProvice().subscribe(res => this.Province = res as []);
    this.service.getDirection().subscribe(res => this.Direction = res as []);
    this.service.getTypeOfProperty().subscribe(res => this.TypeOfProperty = res as []);
    this.service.getEvaluationStatus().subscribe(res => this.EvaluationStatus = res as []);
    this.service.getHotSellProperties().subscribe(res => this.HotSellProperties = res as []);
    this.service.getHotRentProperties().subscribe(res => this.HotRentProperties = res as []);

    $('#myCarousel').carousel({
      interval: 5000
    });

    $('.carousel .carousel-item').each(function () {
      var minPerSlide = 4;
      var next = $(this).next();
      if (!next.length) {
        next = $(this).siblings(':first');
      }
      next.children(':first-child').clone().appendTo($(this));

      for (var i = 0; i < minPerSlide; i++) {
        next = next.next();
        if (!next.length) {
          next = $(this).siblings(':first');
        }

        next.children(':first-child').clone().appendTo($(this));
      }
    });
  }

  OnClick(id: number) {
    this.router.navigate(['/ChiTiet/' + id]);
  }

  OnClicks() {
    let typeOfProperty;
    let price;
    let area;
    let direction;
    let roomNumber;
    let evaluationStatus;
    let province;
    let district;
    let ward;
    if(this.formModels.TypeOfProperty != 0){
      typeOfProperty = " " + this.formModels.TypeOfProperty;
    }
    else{
      typeOfProperty = '';
    }
    if(this.formModels.Price != 0){
      price = " " + this.formModels.Price;
    }
    else{
      price = '';
    }
    if(this.formModels.Area != 0){
      area = " " + this.formModels.Area;
    }
    else{
      area = '';
    }
    if(this.formModels.Direction != 0){
      direction = " hướng " + this.formModels.Direction;
    }
    else{
      direction = '';
    }
    if(this.formModels.RoomNumber != 0){
      roomNumber = "" + this.formModels.RoomNumber;
    }
    else{
      roomNumber = '';
    }
    if(this.formModels.EvaluationStatus != 0){
      evaluationStatus = " " + this.formModels.EvaluationStatus;
    }
    else{
      evaluationStatus = '';
    }
    if(this.formModels.Province != 0){
      province = " " + this.formModels.Province;
    }
    else{
      province = '';
    }
    if(this.formModels.District != 0){
      district = " " + this.formModels.District;
    }
    else{
      district = '';
    }
    if(this.formModels.Ward != 0){
      ward = " " + this.formModels.Ward;
    }
    else{
      ward = '';
    }
    let findSearch = this.formModels.Find + typeOfProperty + price + area + direction + roomNumber + evaluationStatus + province + district + ward;
    console.log(findSearch)
    this.router.navigate(['/TimKiem/' + findSearch]);
  }

  GetProvinceId(event) {
    let id = event.target.value;
    return this.service.getDistrict(id).subscribe(res => this.District = res as []);
  }

  GetDistrictId(event) {
    let id = event.target.value;
    return this.service.getWard(id).subscribe(res => this.Ward = res as []);
  }

  Sell() {
    this.router.navigateByUrl('/NhaDatBan');
  }

  Rent() {
    this.router.navigateByUrl('/NhaDatThue');
  }

  subStr(data: string) {
    return (data.substring(0, 4)) !== 'http';
  }
}