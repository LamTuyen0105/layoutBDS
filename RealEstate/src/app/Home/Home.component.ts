import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
// import * as $ from "jquery";

import { CommonService } from 'src/app/Core/Service/Common.service';

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
    this.router.navigateByUrl('/TimKiem');
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
}