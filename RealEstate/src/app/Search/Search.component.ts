import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { ElasticSearchService } from 'src/app/Core/ElasticSearch/ElasticSearch.service';
@Component({
  selector: 'app-Search',
  templateUrl: './Search.component.html',
  styleUrls: ['./Search.component.css']
})
export class SearchComponent implements OnInit {

  Properties: any[];
  formData = {
    page: 1,
    pageSize: 18,
    query: '',
    isDelete: false
}
  totalRow: number;
  image: string;
  title: string;

  constructor(private router: Router, private service: ElasticSearchService, private currentRoute: ActivatedRoute) { }

  ngOnInit() {
    this.getPageFromService(this.formData.page);
    let finds = this.currentRoute.snapshot.paramMap.get('find');
    this.title = finds;
  }

  getPageFromService(pages) {
    this.formData.page = pages-1;
    let finds = this.currentRoute.snapshot.paramMap.get('find');
    this.formData.query = finds;
    this.service.ElasticSearch(this.formData).subscribe((response: any) => {
      this.Properties = response.results;
      this.totalRow = response.total;
    });
    window.scroll(0,0);
  }

  OnClick(id: number) {
    this.router.navigate(['/ChiTietTimKiem/' + id]);
  }

  OnClicks() {
    this.router.navigateByUrl('/DangTin');
  }

  subStr(data: string){
    return (data.substring(0,4)) !== 'http';
  }
}
