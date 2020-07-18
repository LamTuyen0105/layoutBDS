import { Component, OnInit } from '@angular/core';
import { ElementRef, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { DatePipe } from '@angular/common';

import { CommonService } from 'src/app/Core/Service/Common.service';
import { PostServiceService } from 'src/app/Core/PostService/PostService.service';

@Component({
  selector: 'app-Post',
  templateUrl: './Post.component.html',
  styleUrls: ['./Post.component.css']
})
export class PostComponent implements OnInit {

  Province = [];
  District = [];
  Ward = [];
  Direction = [];
  TypeOfProperties = [];
  TypeOfTransactions = [];
  EvaluationStatus = [];
  LegalPaper = [];

  formModels = {
    Length: 0,
    Width: 0,
    Area: 0
  };
  latitude: number;
  longitude: number;
  zoom: number;
  imageUrl: string = "";
  fileToUpload: File = null;
  form: FormGroup;
  date = new Date;
  endDate = new Date;
  maxDate = new Date;

  @ViewChild('mapRef', { static: true }) mapElement: ElementRef;
  constructor(private service: CommonService, private services: PostServiceService, private fb: FormBuilder, private datePipe: DatePipe) {
    this.renderMap();
    this.form = this.fb.group({
      Title: [''],
      TypeOfTransactionId: [0],
      TypeOfPropertyId: [0],
      Provinces: [0],
      Districts: [0],
      WardId: [0],
      ApartmentNumber: [''],
      StreetNames: [''],
      Description: [''],
      HouseDirectionId: [0],
      LegalPapersId: [0],
      NumberOfBedrooms: [0],
      NumberOfWCs: [0],
      Length: [0],
      Width: [0],
      Facade: [0],
      NumberOfStoreys: [''],
      Area: [null],
      Price: [0],
      EvaluationStatusId: [0],
      StartDate: [Date.now()],
      EndDate: [Date.now()],
      ContactName: [''],
      EmailContact: [''],
      ContactPhone: [''],
      ThumbnailImage: [null]
    })
  }


  ngOnInit() {
    this.service.getProvice().subscribe(res => this.Province = res as []);
    this.service.getDirection().subscribe(res => this.Direction = res as []);
    this.service.getTypeOfProperty().subscribe(res => this.TypeOfProperties = res as []);
    this.service.getTypeOfTransaction().subscribe(res => this.TypeOfTransactions = res as []);
    this.service.getEvaluationStatus().subscribe(res => this.EvaluationStatus = res as []);
    this.service.getLegalPaper().subscribe(res => this.LegalPaper = res as []);
    this.setCurrentLocation();
    this.maxDate.setDate(this.maxDate.getDate() + 30);
  }


  Area() {
    if (this.formModels.Length != null && this.formModels.Width != null) {
      this.formModels.Area = parseFloat((this.formModels.Length * this.formModels.Width).toFixed(2));
    }
    const area = this.formModels.Area;
    this.form.patchValue({
      Area: area
    });
    this.form.get('Area').updateValueAndValidity()
  }

  onSubmit() {
    this.form.value.Area = this.formModels.Area;
    this.form.value.TypeOfPropertyId = Number(this.form.value.TypeOfPropertyId);
    this.form.value.TypeOfTransactionId = Number(this.form.value.TypeOfTransactionId);
    this.form.value.WardId = Number(this.form.value.WardId);
    this.form.value.HouseDirectionId = Number(this.form.value.HouseDirectionId);
    this.form.value.LegalPapersId = Number(this.form.value.LegalPapersId);
    this.form.value.EvaluationStatusId = Number(this.form.value.EvaluationStatusId);
    console.log(this.form.value)
    var formData: any = new FormData();
    formData.append("Title", this.form.get('Title').value);
    formData.append("TypeOfTransactionId", this.form.get('TypeOfTransactionId').value);
    formData.append("TypeOfPropertyId", this.form.get('TypeOfPropertyId').value);
    formData.append("WardId", this.form.get('WardId').value);
    formData.append("ApartmentNumber", this.form.get('ApartmentNumber').value);
    formData.append("StreetNames", this.form.get('StreetNames').value);
    formData.append("Description", this.form.get('Description').value);
    formData.append("HouseDirectionId", this.form.get('HouseDirectionId').value);
    formData.append("LegalPapersId", this.form.get('LegalPapersId').value);
    formData.append("NumberOfBedrooms", this.form.get('NumberOfBedrooms').value);
    formData.append("NumberOfWCs", this.form.get('NumberOfWCs').value);
    formData.append("Length", this.form.get('Length').value);
    formData.append("Width", this.form.get('Width').value);
    formData.append("Facade", this.form.get('Facade').value);
    formData.append("NumberOfStoreys", this.form.get('NumberOfStoreys').value);
    formData.append("Area", this.form.get('Area').value);
    formData.append("Price", this.form.get('Price').value);
    formData.append("EvaluationStatusId", this.form.get('EvaluationStatusId').value);
    formData.append("StartDate", this.form.get('StartDate').value);
    formData.append("EndDate", this.form.get('EndDate').value);
    formData.append("ContactName", this.form.get('ContactName').value);
    formData.append("EmailContact", this.form.get('EmailContact').value);
    formData.append("ContactPhone", this.form.get('ContactPhone').value);
    formData.append("ThumbnailImage", this.form.get('ThumbnailImage').value);
    this.services.postProperty(formData).subscribe(
      (res: any) => {
        console.log(res);
        this.form.reset();
      });
  }

  GetProvinceId(event) {
    let id = event.target.value;
    return this.service.getDistrict(id).subscribe(res => this.District = res as []);
  }

  GetDistrictId(event) {
    let id = event.target.value;
    if (id != "")
      return this.service.getWard(id).subscribe(res => this.Ward = res as []);
  }

  GetStartDate(event) {
    this.endDate = event.target.value;
  }

  renderMap() {

  }

  handleFileInput(file: FileList, event) {
    this.fileToUpload = file.item(0);
    const files = (event.target as HTMLInputElement).files[0];
    var reader = new FileReader();
    reader.onload = (event: any) => {
      this.imageUrl = event.target.result;
    }
    this.form.patchValue({
      ThumbnailImage: files
    });
    this.form.get('ThumbnailImage').updateValueAndValidity()
    reader.readAsDataURL(this.fileToUpload);
  }

  private setCurrentLocation() {
    if ('geolocation' in navigator) {
      navigator.geolocation.getCurrentPosition((position) => {
        this.latitude = position.coords.latitude;
        this.longitude = position.coords.longitude;
        this.zoom = 10;
      });
    }
  }
}