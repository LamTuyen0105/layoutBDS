import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AgmCoreModule } from '@agm/core';
import { FormsModule, ReactiveFormsModule, } from '@angular/forms';
import { RecaptchaModule, RECAPTCHA_SETTINGS, RecaptchaSettings } from 'ng-recaptcha';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DatePipe} from '@angular/common';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MasterpageComponent } from './Masterpage/Masterpage.component';
import { HomeComponent } from './Home/Home.component';
import { PostComponent } from './Post/Post.component';
import { FillterComponent } from './Fillter/Fillter.component';
import { RegisterComponent } from './Register/Register.component';
import { DetailComponent } from './Detail/Detail.component';
import { NewsComponent } from './News/News.component';
import { MostviewComponent } from './Mostview/Mostview.component';
import { NewsdetailsComponent } from './Newsdetails/Newsdetails.component';
import { SearchComponent } from './Search/Search.component';
import { ProfileComponent } from './Profile/Profile.component';
import { UserServiceService } from './Core/UserService/UserService.service';
import { AuthInterceptor } from './Auth/auth.interceptor';
import { MyPostComponent } from './MyPost/MyPost.component';
import { EditMyPostComponent } from './EditMyPost/EditMyPost.component';
import { SellComponent } from './Sell/Sell.component';
import { ForRentComponent } from './ForRent/ForRent.component';
import { NeedToBuyComponent } from './NeedToBuy/NeedToBuy.component';
import { NeedToRentComponent } from './NeedToRent/NeedToRent.component';

@NgModule({
   declarations: [
      AppComponent,
      MasterpageComponent,
      HomeComponent,
      PostComponent,
      FillterComponent,
      RegisterComponent,
      DetailComponent,
      NewsComponent,
      MostviewComponent,
      NewsdetailsComponent,
      SearchComponent,
      ProfileComponent,
      MyPostComponent,
      EditMyPostComponent,
      SellComponent,
      ForRentComponent,
      NeedToBuyComponent,
      NeedToRentComponent
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      ReactiveFormsModule,
      FormsModule,
      HttpClientModule,
      RecaptchaModule,
      BrowserAnimationsModule,
      AgmCoreModule.forRoot({
         apiKey:'AIzaSyA-uzx0QUjC9nOoh7hQwJts6Bbdhl48lDo',
         libraries: ["places", "geometry"]
      })
   ],
   providers: [DatePipe, UserServiceService, {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    {
      provide: RECAPTCHA_SETTINGS,
      useValue: {
        siteKey: '6LcoSAEVAAAAAJ5WwKfibd64P9tfLae5Rt-bBqOf',
      } as RecaptchaSettings,
    }],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }