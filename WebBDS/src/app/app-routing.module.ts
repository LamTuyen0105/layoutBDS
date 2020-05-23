import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
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
import { NewsdetailComponent } from './newsdetail/newsdetail.component';
import { ProjectComponent} from './project/project.component';
import { ChangepasswordComponent} from './profile/changepassword/changepassword.component';
import { ManagerpostnewsComponent } from './profile/managerpostnews/managerpostnews.component';

const routes: Routes = [{ path: '', redirectTo: 'Home', pathMatch: 'full' },
{ path: 'Master', component: MasterpageComponent },
{
  path: 'Main',
  component: MainComponent,
  children: [
    {
      path: 'Registration',
      component: RegistrationComponent
    },
    {
      path: 'Login',
      component: LoginComponent
    }
  ]
},
{ path: 'Detail', component: DetailComponent },
{ path: 'Home', component: HomeComponent },
{
  path: 'PostNews',
  component: PostnewsComponent,
  children: [
    {
      path: 'Sell',
      component: SellComponent
    },
    {
      path: 'Buy',
      component: BuyComponent
    }
  ]
},
{ path: 'Profile', 
  component: ProfileComponent, 
  children: [
    {
      path: 'ManagerPostNews',
      component: ManagerpostnewsComponent
    },
    {
      path: 'ChangePassword',
      component: ChangepasswordComponent
    }
  ]
},
{ path: 'News', component: NewsComponent },
{ path: 'NewsDetail', component: NewsdetailComponent },
{ path: 'Project', component: ProjectComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
