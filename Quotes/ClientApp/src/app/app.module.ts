import {NgModule, Provider} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginPageComponent } from './Account/login-page/login-page.component';
import { RegPageComponent } from './Account/reg-page/reg-page.component';
import { HomePageComponent } from './Home/home-page/home-page.component';
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {AuthService} from "./Services/auth.service";
import {DataService} from "./Services/data.service";
import {JwtInterceptor} from "./Services/jwt.interceptor";


const INTERCEPTOR_PROVIDER: Provider = {
  provide: HTTP_INTERCEPTORS,
  useClass: JwtInterceptor,
  multi: true
};

@NgModule({
  declarations: [
    AppComponent,
    LoginPageComponent,
    RegPageComponent,
    HomePageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [AuthService, DataService, INTERCEPTOR_PROVIDER],
  bootstrap: [AppComponent]
})
export class AppModule { }
