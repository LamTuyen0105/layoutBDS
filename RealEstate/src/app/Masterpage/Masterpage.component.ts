import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-Masterpage',
  templateUrl: './Masterpage.component.html',
  styleUrls: ['./Masterpage.component.css']
})
export class MasterpageComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit() {
  }

  OnClick(){
    this.router.navigateByUrl('/i/DangTin');
  }
}

