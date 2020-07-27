import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { MainService } from 'src/app/share/main.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  MainForms: FormArray = this.fb.array([]);
  Property = [];
  notification = null;

  constructor(private fb: FormBuilder, private service: MainService, private router: Router, private toastr: ToastrService) { }

  ngOnInit() {
    
  }
  onClick() {
    localStorage.removeItem('token');
    this.router.navigate(['/main/login']);
  }
}

