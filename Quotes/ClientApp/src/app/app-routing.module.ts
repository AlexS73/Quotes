import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {HomePageComponent} from "./Home/home-page/home-page.component";
import {RegPageComponent} from "./Account/reg-page/reg-page.component";
import {LoginPageComponent} from "./Account/login-page/login-page.component";
import {AuthGuardService} from "./Services/auth.guard.service";

const routes: Routes = [
  {path: '', component: HomePageComponent, canActivate: [ AuthGuardService ]},
  {path: 'registration', component: RegPageComponent, canActivate: [ AuthGuardService ]},
  {path: 'login', component: LoginPageComponent, canActivate: [ AuthGuardService ]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [AuthGuardService]
})
export class AppRoutingModule { }
