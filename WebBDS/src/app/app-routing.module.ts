import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainComponent } from './main/main.component';
import { LoginComponent } from './main/login/login.component';
import { DetailhomeComponent } from './detail/detailhome/detailhome.component';
import { AdminComponent } from './admin/admin.component';
import { PostComponent } from './post/post.component';
import { from } from 'rxjs';


const routes: Routes = [
  { path: '', redirectTo: 'main/login', pathMatch: 'full' },
  {
    path: 'main',
    component: MainComponent,
    children: [
      {
        path: 'login',
        component: LoginComponent
      }
    ]
  },
  { path: 'ChiTiets/:id', component: PostComponent },
  {
    path:'admin',
    component:AdminComponent,
    children: [
      {path:'detailhome',component:DetailhomeComponent},
    ]
  },
  {path: 'post', component:PostComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
