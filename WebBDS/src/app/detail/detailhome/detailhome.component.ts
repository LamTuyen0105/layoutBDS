import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { MainService } from 'src/app/share/main.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-detailhome',
  templateUrl: './detailhome.component.html',
  styles: []
})
export class DetailhomeComponent implements OnInit {

  MainForms: FormArray = this.fb.array([]);
  Property = [];
  notification = null;

  constructor(private fb: FormBuilder, private service: MainService, private router: Router, private toastr: ToastrService) { }

  ngOnInit() {
    this.service.getProperty().subscribe(
      (res => this.Property = res as []))
  }

  OnClick(id: number) {
    this.router.navigate(['/ChiTiets/' + id]);
  }
}

      

