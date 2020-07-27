import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MainService } from 'src/app/share/main.service';
@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {

  Property = [];
  Images = [];
  ImageDefault = [];
  ImageNonDefault = [];

  constructor(private router: Router, private currentRoute: ActivatedRoute, private service: MainService) { }

  ngOnInit() {
    let propertyID = this.currentRoute.snapshot.paramMap.get('id');
    this.service.getProperties(parseInt(propertyID)).subscribe(res => this.Property = res as []);
    this.service.getPropertyImageDefault(parseInt(propertyID)).subscribe(res => this.ImageDefault = res as []);
    this.service.getPropertyImageNonDefault(parseInt(propertyID)).subscribe(res => this.ImageNonDefault = res as []);
  }
  OnClickDelete() {
    let id = this.currentRoute.snapshot.paramMap.get('id');
    this.service.deletteProperties(id).subscribe(res => {
      console.log(res)
      this.router.navigateByUrl('/admin/detailhome')
      },
      error => console.log(error),     
    );      
  }
  subStr(data: string) {
    return (data.substring(0, 4)) !== 'http';
  }
}
