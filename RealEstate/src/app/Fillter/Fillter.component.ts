import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-Fillter',
  templateUrl: './Fillter.component.html',
  styleUrls: ['./Fillter.component.css']
})
export class FillterComponent implements OnInit {
  formModels = {
    Find: ''
    };
    title: string;

  constructor(private router: Router,  private currentRoute: ActivatedRoute) { }

  ngOnInit() {
    let finds = this.currentRoute.snapshot.paramMap.get('find');
    this.title = finds;
  }

  OnClicks() {
    let find = this.formModels.Find;
    this.router.navigateByUrl('/TimKiem/' + find);
  }
}