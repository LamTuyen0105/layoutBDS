import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { CommonService } from 'src/app/Core/Service/Common.service';
@Component({
  selector: 'app-Search',
  templateUrl: './Search.component.html',
  styleUrls: ['./Search.component.css']
})
export class SearchComponent implements OnInit {

  HotSellProperties = [];

  constructor(private router: Router, private service: CommonService) { }

  ngOnInit() {
    this.service.getHotSellProperties().subscribe(res => this.HotSellProperties = res as []);
  }
  OnClick(id: number) {
    this.router.navigate(['/ChiTiet/'+ id]);
  }

  OnClicks(){
    this.router.navigateByUrl('/i/DangTin');
  }

}
