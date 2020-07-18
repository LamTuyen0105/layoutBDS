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

  constructor(private router: Router, private currentRoute: ActivatedRoute, private service: CommonService) { }

  ngOnInit() {
    let propertyID = this.currentRoute.snapshot.paramMap.get('id');
    this.service.getProperties(parseInt(propertyID)).subscribe(res => this.Property = res as []);
    this.service.getPropertyImage(parseInt(propertyID)).subscribe(res => this.Images = res as []);
    this.service.getPropertyImageDefault(parseInt(propertyID)).subscribe(res => this.ImageDefault = res as []);
    this.service.getPropertyImageNonDefault(parseInt(propertyID)).subscribe(res => this.ImageNonDefault = res as []);
  }

}
