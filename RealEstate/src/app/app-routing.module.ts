import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { MasterpageComponent } from './Masterpage/Masterpage.component';
import { HomeComponent } from './Home/Home.component';
import { PostComponent } from './Post/Post.component';
import { DetailComponent } from './Detail/Detail.component';
import { NewsComponent } from './News/News.component';
import { NewsdetailsComponent } from './Newsdetails/Newsdetails.component';
import { SearchComponent } from './Search/Search.component';
import { ProfileComponent } from './Profile/Profile.component';
import { AuthGuard } from './Auth/Auth.guard';
import { MyPostComponent } from './MyPost/MyPost.component';
import { EditMyPostComponent } from './EditMyPost/EditMyPost.component';
import { SellComponent } from './Sell/Sell.component';
import { ForRentComponent } from './ForRent/ForRent.component';
import { NeedToBuyComponent } from './NeedToBuy/NeedToBuy.component';
import { NeedToRentComponent } from './NeedToRent/NeedToRent.component';

const routes: Routes = [{ path: '', redirectTo: 'TrangChu', pathMatch: 'full' },
  { path: 'DPSRealestate', component: MasterpageComponent },
  { path: 'TrangChu', component: HomeComponent },
  { path: 'NhaDatBan', component: SellComponent },
  { path: 'NhaDatThue', component: ForRentComponent },
  { path: 'CanMua', component: NeedToBuyComponent },
  { path: 'CanThue', component: NeedToRentComponent },
  { path: 'TinTuc', component: NewsComponent },
  { path: 'DangTin', component: PostComponent },
  { path: 'ChiTiet/:id', component: DetailComponent },
  { path: 'ChiTietTinTuc', component: NewsdetailsComponent },
  { path: 'TimKiem', component: SearchComponent },
  { path: 'ThongTin', component: ProfileComponent, canActivate:[AuthGuard] },
  { path: 'QuanLyTin', component: MyPostComponent, canActivate:[AuthGuard] },
  { path: 'TinChinhSua/:id', component: EditMyPostComponent, canActivate:[AuthGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
