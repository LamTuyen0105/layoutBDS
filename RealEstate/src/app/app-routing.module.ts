import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { MasterpageComponent } from './Masterpage/Masterpage.component';
import { HomeComponent } from './Home/Home.component';
import { PostComponent } from './Post/Post.component';
import { MasteradsComponent } from './Masterads/Masterads.component';
import { DetailComponent } from './Detail/Detail.component';
import { NewsComponent } from './News/News.component';
import { NewsdetailsComponent } from './Newsdetails/Newsdetails.component';

const routes: Routes = [{ path: '', redirectTo: 'TrangChu', pathMatch: 'full' },
  { path: 'DPSRealestate', component: MasterpageComponent },
  { path: 'TrangChu', component: HomeComponent },
  { path: 'i',
    component: MasteradsComponent,
    children: [
      { path: 'DangTin', component: PostComponent },
      { path: 'TinTuc', component: NewsComponent },
    ]
  },
  { path: 'ChiTiet', component: DetailComponent },
  { path: 'ChiTietTinTuc', component: NewsdetailsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
