import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AgmCoreModule } from '@agm/core';
import { RecaptchaModule, RecaptchaFormsModule } from 'ng-recaptcha';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MasterpageComponent } from './Masterpage/Masterpage.component';
import { HomeComponent } from './Home/Home.component';
import { PostComponent } from './Post/Post.component';
import { MasteradsComponent } from './Masterads/Masterads.component';
import { FillterComponent } from './Fillter/Fillter.component';
import { RegisterComponent } from './Register/Register.component';
import { DetailComponent } from './Detail/Detail.component';
import { NewsComponent } from './News/News.component';
import { MostviewComponent } from './Mostview/Mostview.component';
import { NewsdetailsComponent } from './Newsdetails/Newsdetails.component';

@NgModule({
   declarations: [
      AppComponent,
      MasterpageComponent,
      HomeComponent,
      PostComponent,
      MasteradsComponent,
      FillterComponent,
      RegisterComponent,
      DetailComponent,
      NewsComponent,
      MostviewComponent,
      NewsdetailsComponent
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      RecaptchaModule,
      RecaptchaFormsModule,
      AgmCoreModule.forRoot({apiKey:""})
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
