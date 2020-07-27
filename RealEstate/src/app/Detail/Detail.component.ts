import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import {CommonService } from 'src/app/Core/Service/Common.service';

@Component({
  selector: 'app-Detail',
  templateUrl: './Detail.component.html',
  styleUrls: ['./Detail.component.css']
})
export class DetailComponent implements OnInit {

  Property = [];
  Images = [];
  ImageDefault = [];
  ImageNonDefault = [];
  latitude: number = 10.823099;
  longitude: number = 106.629664;
  zoom: number = 15;

  constructor(private router: Router, private currentRoute: ActivatedRoute, private service: CommonService) { }

  ngOnInit() {
    let propertyID = this.currentRoute.snapshot.paramMap.get('id');
    this.service.getProperties(parseInt(propertyID)).subscribe(res => {
      this.Property = res as [];
      this.latitude = this.Property[0].lat;
      this.longitude = this.Property[0].lng;
    });
    this.service.getPropertyImage(parseInt(propertyID)).subscribe(res => this.Images = res as []);
    this.service.getPropertyImageDefault(parseInt(propertyID)).subscribe(res => this.ImageDefault = res as []);
    this.service.getPropertyImageNonDefault(parseInt(propertyID)).subscribe(res => this.ImageNonDefault = res as []);
    this.setCurrentLocation();
  }

  private setCurrentLocation() {
    if ('geolocation' in navigator) {
      navigator.geolocation.getCurrentPosition((position) => {
        this.latitude = position.coords.latitude;
        this.longitude = position.coords.longitude;
        this.zoom = 10;
      });
    }
  }

  subStr(data: string){
    return (data.substring(0,4)) !== 'http';
  }
}
