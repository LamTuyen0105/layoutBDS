import { Component, OnInit, NgZone, ElementRef, ViewChild } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, FormArray, NgForm } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { MapsAPILoader, MouseEvent } from '@agm/core';

import { CommonService } from 'src/app/Core/Service/Common.service';
import { PostServiceService } from 'src/app/Core/PostService/PostService.service';
import { UserServiceService } from 'src/app/Core/UserService/UserService.service';

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
    Area: 0,
  };
  latitude: number = 10.762622;
  longitude: number = 106.660172;
  zoom: number = 10;
  // imageUrl: string = "";
  imageUrl = [];
  myFiles: string[] = [];
  fileToUpload: File = null;
  form: FormGroup;
  date = new Date;
  endDate = new Date;
  maxDate = new Date;
  address: string;
  private geoCoder;
  formemail = {
    email: ''
  }
  checkemail = [];
  emailerror: boolean = false;

  @ViewChild('search', { static: true }) searchElementRef: ElementRef;
  constructor(private service: CommonService, private services: PostServiceService, private fb: FormBuilder, private datePipe: DatePipe, public serviceuser: UserServiceService, private mapsAPILoader: MapsAPILoader,
    private ngZone: NgZone) {
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
      EndDate: [this.maxDate.toISOString()],
      ContactName: [''],
      EmailContact: [''],
      ContactPhone: [''],
      ThumbnailImage: [null],
      Lat: [10.823099],
      Lng: [106.629664],
      checkReCaptCha: [null]
    })
  }


  ngOnInit() {
    this.mapsAPILoader.load().then(() => {
      this.setCurrentLocation();
      this.geoCoder = new google.maps.Geocoder;
      let autocomplete = new google.maps.places.Autocomplete(this.searchElementRef.nativeElement);
      autocomplete.addListener("place_changed", () => {
        this.ngZone.run(() => {
          let place: google.maps.places.PlaceResult = autocomplete.getPlace();
          if (place.geometry === undefined || place.geometry === null) {
            return;
          }
          this.latitude = place.geometry.location.lat();
          this.longitude = place.geometry.location.lng();
          this.zoom = 12;
        });
      });
    });
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
    for (var i = 0; i < this.myFiles.length; i++) {
      formData.append("ThumbnailImage", this.myFiles[i]);
    }
    formData.append("Lat", this.latitude);
    formData.append("Lng", this.longitude);
    formData.append('checkReCaptCha', this.form.get('checkReCaptCha').value);
    this.services.postProperty(formData).subscribe(
      (res: any) => {
        console.log(res);
        for (var pair of formData.entries()) {
          console.log(pair[0] + ', ' + pair[1]);
        }
        this.form.reset();
      });
  }

  onSubmits() {
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
    for (var i = 0; i < this.myFiles.length; i++) {
      formData.append("ThumbnailImage", this.myFiles[i]);
    }
    formData.append("Lat", this.latitude);
    formData.append("Lng", this.longitude);
    formData.append('checkReCaptCha', this.form.get('checkReCaptCha').value);
    this.serviceuser.postPropertyUser(formData).subscribe(
      (res: any) => {
        console.log(res);
        this.form.reset();
      },
      err => {
        console.log("chưa đăng nhập");
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

  async resolved(captchaResponse: string, res) {
    console.log(`Resolved response token: ${captchaResponse}`);
    this.form.patchValue({
      checkReCaptCha: captchaResponse
    });
    this.form.get('checkReCaptCha').updateValueAndValidity()
  }

  handleFileInput(file: FileList, event) {
    if (event.target.files && event.target.files[0]) {
      var filesAmount = event.target.files.length;
      for (let i = 0; i < filesAmount; i++) {
        this.fileToUpload = file.item(i);
        const files = (event.target as HTMLInputElement).files[i];
        var reader = new FileReader();
        reader.onload = (event: any) => {
          this.imageUrl.push(event.target.result);
        }
        reader.readAsDataURL(event.target.files[i]);
      }
      for (var i = 0; i < event.target.files.length; i++) {
        this.myFiles.push(event.target.files[i]);
      }
      console.log(this.myFiles)
    }
  }

  private setCurrentLocation() {
    if ('geolocation' in navigator) {
      navigator.geolocation.getCurrentPosition((position) => {
        this.latitude = position.coords.latitude;
        this.longitude = position.coords.longitude;
        this.zoom = 8;
        this.getAddress(this.latitude, this.longitude);
      });
    }
  }

  markerDragEnd($event: MouseEvent) {
    console.log($event);
    this.latitude = $event.coords.lat;
    this.longitude = $event.coords.lng;
    this.getAddress(this.latitude, this.longitude);
  }

  getAddress(latitude, longitude) {
    this.geoCoder.geocode({ 'location': { lat: latitude, lng: longitude } }, (results, status) => {
      console.log(results);
      console.log(status);
      if (status === 'OK') {
        if (results[0]) {
          this.zoom = 12;
          this.address = results[0].formatted_address;
          this.form.patchValue({
            Lat: latitude,
            Lng: longitude
          });
          this.form.get('Lat').updateValueAndValidity()
          this.form.get('Lng').updateValueAndValidity()
        } else {
          window.alert('No results found');
        }
      } else {
        window.alert('Geocoder failed due to: ' + status);
      }
    });
  }

  VerifyEmail() {
    this.serviceuser.VerifyEmail(this.formemail.email).subscribe((res: any) => {
      if (res.status == 0) {
        this.emailerror = true;
      }
      else {
        this.emailerror = false;
      }
      console.log(res.status)
    });
    console.log(this.emailerror)
  }

}