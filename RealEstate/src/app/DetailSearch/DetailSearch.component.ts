import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import {CommonService } from 'src/app/Core/Service/Common.service';

@Component({
  selector: 'app-DetailSearch',
  templateUrl: './DetailSearch.component.html',
  styleUrls: ['./DetailSearch.component.css']
})
export class DetailSearchComponent implements OnInit {

  Property = [];
  Images = [];
  ImageDefault = [];
  ImageNonDefault = [];

  constructor(private router: Router, private currentRoute: ActivatedRoute, private service: CommonService) { }

  ngOnInit() {
    let propertyID = this.currentRoute.snapshot.paramMap.get('id');
    this.service.getElsticSearchProperties(parseInt(propertyID)).subscribe(res => this.Property = res as []);
    this.service.getPropertyImage(parseInt(propertyID)).subscribe(res => this.Images = res as []);
    this.service.getPropertyImageDefault(parseInt(propertyID)).subscribe(res => this.ImageDefault = res as []);
    this.service.getPropertyImageNonDefault(parseInt(propertyID)).subscribe(res => this.ImageNonDefault = res as []);
  }

  subStr(data: string){
    return (data.substring(0,4)) !== 'http';
  }

}
