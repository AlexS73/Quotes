import {APP_INITIALIZER, NgModule, Provider} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginPageComponent } from './Account/login-page/login-page.component';
import { RegPageComponent } from './Account/reg-page/reg-page.component';
import { HomePageComponent } from './Home/home-page/home-page.component';
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {AuthService} from "./Shared/Services/auth.service";
import {DataService} from "./Shared/Services/data.service";
import {JwtInterceptor} from "./Shared/Services/jwt.interceptor";
import {ReactiveFormsModule} from "@angular/forms";
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {appInitializer} from "./Shared/Services/app.initializer";


const INTERCEPTOR_PROVIDER: Provider = {
  provide: HTTP_INTERCEPTORS,
  useClass: JwtInterceptor,
  multi: true
};

const INITIALIZER_PROVIDER: Provider = {
  provide: APP_INITIALIZER,
  useFactory: appInitializer,
  multi: true,
  deps: [AuthService]
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
    HttpClientModule,
    ReactiveFormsModule,
    NgbModule,

  ],
  providers: [AuthService, DataService, INTERCEPTOR_PROVIDER, INITIALIZER_PROVIDER],
  bootstrap: [AppComponent]
})
export class AppModule { }
