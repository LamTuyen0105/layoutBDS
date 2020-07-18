import { Component, OnInit } from '@angular/core';

import { UserServiceService } from 'src/app/Core/UserService/UserService.service';


@Component({
  selector: 'app-Profile',
  templateUrl: './Profile.component.html',
  styleUrls: ['./Profile.component.css']
})
export class ProfileComponent implements OnInit {
  userDetails;

  constructor(public service: UserServiceService) { }

  ngOnInit() {
    this.service.getUserProfile().subscribe(
      res => {
        this.userDetails = res;
      },
      err => {
        console.log(err);
      },
    );
  }

}
