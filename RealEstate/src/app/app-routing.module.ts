import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { MasterpageComponent } from './Masterpage/Masterpage.component';
import { HomeComponent } from './Home/Home.component';


const routes: Routes = [{ path: '', redirectTo: 'Home', pathMatch: 'full' },
{ path: 'Masterpage', component: MasterpageComponent },
// {
//   path: 'Main',
//   component: MainComponent,
//   children: [
//     {
//       path: 'Registration',
//       component: RegistrationComponent
//     },
//     {
//       path: 'Login',
//       component: LoginComponent
//     }
//   ]
// },
// { path: 'Detail', component: DetailComponent },
{ path: 'Home', component: HomeComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
