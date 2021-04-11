import { Component, OnInit } from '@angular/core';
import {DataService} from "../../Shared/Services/data.service";
import {IValCursModel} from "../../Shared/Interfaces/IValCurs.model";
import {NgbDate} from "@ng-bootstrap/ng-bootstrap";
import {AuthService} from "../../Shared/Services/auth.service";

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent {
  ValCurs: IValCursModel;
  token: any;
  user =  { email: '' };



  constructor(private dataService: DataService, private authService: AuthService) {
    this.ValCurs = new IValCursModel();
    this.token = authService.token;
    this.user = authService.GetUser;
  }


  onDateSelect($event: NgbDate) {
    this.dataService.GetValutes(new Date($event.year, $event.month-1, $event.day))
      .subscribe(response => {
        this.ValCurs = response;
      })
  }

  refreshToken() {
    this.authService.RefreshToken()
      .subscribe(_=>{
        this.token = this.authService.token;
      });
  }


  LogOut() {
    this.authService.LogOut();
  }
}
