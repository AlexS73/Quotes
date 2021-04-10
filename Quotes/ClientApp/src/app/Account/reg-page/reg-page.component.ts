import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup} from "@angular/forms";

@Component({
  selector: 'app-reg-page',
  templateUrl: './reg-page.component.html',
  styleUrls: ['./reg-page.component.scss']
})
export class RegPageComponent implements OnInit {

  loginForm : FormGroup

  constructor() { }

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      "UserName" : new FormControl(),
      "Password" : new FormControl(),
      "ConfirmPassword" : new FormControl()
    });
  }

  Submit() {
    console.log(this.loginForm);
  }

}
