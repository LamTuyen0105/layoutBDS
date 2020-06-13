import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-News',
  templateUrl: './News.component.html',
  styleUrls: ['./News.component.css']
})
export class NewsComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit() {
  }
  
  Click(){
    this.router.navigateByUrl('/ChiTietTinTuc');
  }
}
