import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { CommonService } from 'src/app/Core/Service/Common.service';

@Component({
  selector: 'app-Sell',
  templateUrl: './Sell.component.html',
  styleUrls: ['./Sell.component.css']
})
export class SellComponent implements OnInit {
  Properties : any[];
  pageIndex: number = 1;
  pageSize: number = 18;
  typeofproperty: number = 1;
  totalRow: number;

  constructor(private router: Router, private service: CommonService) {  }

  ngOnInit() {
    this.getPageFromService(this.pageIndex);
    
  }

  getPageFromService(pageIndex) {
    this.service.getViewPaging( pageIndex, this.pageSize, this.typeofproperty).subscribe((response: any) => {
      this.Properties = response.items;
      this.totalRow = response.totalRecords;           
    });
    window.scroll(0,0);
  }

  OnClick(id: number) {
    this.router.navigate(['/ChiTiet/' + id]);
  }

  OnClicks(){
    this.router.navigateByUrl('/DangTin');
  }

  subStr(data: string){
    return (data.substring(0,4)) !== 'http';
  }
}
