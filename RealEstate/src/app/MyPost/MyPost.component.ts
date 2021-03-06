import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { UserServiceService } from 'src/app/Core/UserService/UserService.service';
import { PostServiceService } from 'src/app/Core/PostService/PostService.service';

@Component({
  selector: 'app-MyPost',
  templateUrl: './MyPost.component.html',
  styleUrls: ['./MyPost.component.css']
})
export class MyPostComponent implements OnInit {

  userProperty;
  
  constructor(private router: Router, public service: UserServiceService, private services: PostServiceService) { }

  ngOnInit() {
    this.service.getPropertyUser().subscribe(
      res => {
        this.userProperty = res;
        console.log(res)
      },
      err => {
        console.log(err);
      },
    );
  }

  OnClicks(id: number) {
    this.router.navigate(['/TinChinhSua/'+ id]);
  }

  OnClickDelete(id: number) {
    if(confirm('Bạn chắc muốn xóa?')){
    this.services.deletteProperties(id).subscribe(res => {
      console.log(res)
      this.service.getPropertyUser().subscribe(
        res => {
          this.userProperty = res;
          console.log(res)
        },
        err => {
          console.log(err);
        },
      );
      },
      error => console.log(error)
    );}      
  }

  subStr(data: string){
    return (data.substring(0,4)) !== 'http';
  }
}
