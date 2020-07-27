import { Component, OnInit, NgZone, ElementRef, ViewChild  } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { DatePipe } from '@angular/common';
import { MapsAPILoader, MouseEvent } from '@agm/core';

import { CommonService } from 'src/app/Core/Service/Common.service';
import { PostServiceService } from 'src/app/Core/PostService/PostService.service';


@Component({
  selector: 'app-EditMyPost',
  templateUrl: './EditMyPost.component.html',
  styleUrls: ['./EditMyPost.component.css']
})
export class EditMyPostComponent implements OnInit {

  Property = [];
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
  latitude: number = 10.762622;
  longitude: number = 106.660172;
  zoom: number = 12;
  imageUrl: string = "";
  fileToUpload: File = null;
  form: FormGroup;
  date = new Date;
  maxDate = new Date;
  address: string;
  private geoCoder;

  @ViewChild('search', { static: true }) searchElementRef: ElementRef;
  constructor(private service: CommonService, private services: PostServiceService, private currentRoute: ActivatedRoute, private fb: FormBuilder, private datePipe: DatePipe,  private mapsAPILoader: MapsAPILoader,
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
      EndDate: [Date.now()],
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
    let propertyID = this.currentRoute.snapshot.paramMap.get('id');
    this.services.getPutProperties(parseInt(propertyID)).subscribe(res => {
      this.Property = res as []
      this.form.controls['Title'].setValue(this.Property[0].title);
      this.form.controls['TypeOfTransactionId'].setValue((this.Property[0].typeOfTransactionId));
      this.form.controls['TypeOfPropertyId'].setValue(this.Property[0].typeOfPropertyId);
      this.form.controls['Provinces'].setValue(this.Property[0].provinceId);
      this.service.getDistrict(this.Property[0].provinceId).subscribe(res => this.District = res as []);
      this.form.controls['Districts'].setValue(this.Property[0].districtId);
      this.service.getWard(this.Property[0].districtId).subscribe(res => this.Ward = res as []);
      this.form.controls['WardId'].setValue(this.Property[0].wardId);
      this.form.controls['ApartmentNumber'].setValue(this.Property[0].apartmentNumber);
      this.form.controls['StreetNames'].setValue(this.Property[0].streetNames);
      this.form.controls['Description'].setValue(this.Property[0].description);
      this.form.controls['HouseDirectionId'].setValue(this.Property[0].houseDirectionId);
      this.form.controls['LegalPapersId'].setValue(this.Property[0].legalPapersId);
      this.form.controls['NumberOfBedrooms'].setValue(this.Property[0].numberOfBedrooms);
      this.form.controls['NumberOfWCs'].setValue(this.Property[0].numberOfWCs);
      this.form.controls['Length'].setValue(this.Property[0].length);
      this.form.controls['Width'].setValue(this.Property[0].width);
      this.form.controls['Facade'].setValue(this.Property[0].facade);
      this.form.controls['NumberOfStoreys'].setValue(this.Property[0].numberOfStoreys);
      this.form.controls['Area'].setValue(this.Property[0].area);
      this.form.controls['Price'].setValue(this.Property[0].price);
      this.form.controls['EvaluationStatusId'].setValue(this.Property[0].evaluationStatusId);
      this.form.controls['StartDate'].setValue(this.datePipe.transform(this.Property[0].startDate, "yyyy-MM-dd"));
      this.form.controls['EndDate'].setValue(this.datePipe.transform(this.Property[0].endDate, "yyyy-MM-dd"));
      this.form.controls['ContactName'].setValue(this.Property[0].contactName);
      this.form.controls['EmailContact'].setValue(this.Property[0].emailContact);
      this.form.controls['ContactPhone'].setValue(this.Property[0].contactPhone);
      this.form.controls['ThumbnailImage'].setValue(this.Property[0].linkName);
      this.latitude = this.Property[0].lat;
      this.longitude = this.Property[0].lng;
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
    formData.append("Lat", this.latitude);
    formData.append("Lng", this.longitude);
    formData.append('checkReCaptCha',this.form.get('checkReCaptCha').value);
    let propertyID = this.currentRoute.snapshot.paramMap.get('id');
    this.services.putProperties(propertyID, formData).subscribe(
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

  async resolved(captchaResponse: string, res) {
    console.log(`Resolved response token: ${captchaResponse}`);
    this.form.patchValue({
      checkReCaptCha: captchaResponse
    });
    this.form.get('checkReCaptCha').updateValueAndValidity()
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
}