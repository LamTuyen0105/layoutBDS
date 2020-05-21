import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MasterpageComponent } from './masterpage/masterpage.component';
import { HomeComponent } from './home/home.component';
import { DetailComponent } from './detail/detail.component';
import { MainComponent } from './main/main.component';
import { LoginComponent } from './main/login/login.component';
import { RegistrationComponent } from './main/registration/registration.component';
import { PostnewsComponent } from './postnews/postnews.component';
import { BuyComponent } from './postnews/buy/buy.component';
import { SellComponent } from './postnews/sell/sell.component';
import { ProfileComponent } from './profile/profile.component';
import { NewsComponent } from './news/news.component';

@NgModule({
   declarations: [
      AppComponent,
      MasterpageComponent,
      HomeComponent,
      DetailComponent,
      MainComponent,
      LoginComponent,
      RegistrationComponent,
      PostnewsComponent,
      BuyComponent,
      SellComponent,
      ProfileComponent,
      NewsComponent
   ],
   imports: [
      BrowserModule,
      AppRoutingModule
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
