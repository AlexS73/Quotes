import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {AuthService} from "../../Shared/Services/auth.service";

@Component({
  selector: 'app-reg-page',
  templateUrl: './reg-page.component.html',
  styleUrls: ['./reg-page.component.scss']
})
export class RegPageComponent implements OnInit {

  regForm : FormGroup
  private loading: boolean;

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    this.regForm = new FormGroup({
      "Email" : new FormControl('',[
        Validators.email,
        Validators.required
      ]),
      "Password" : new FormControl('', [
        Validators.required,
        Validators.minLength(8)
      ]),
      "ConfirmPassword" : new FormControl('', [
      ])
    });
  }

  Submit(e) {
    e.preventDefault();
    const { Email, Password, ConfirmPassword } = this.regForm.value;
    this.loading = true;

    const result = this.authService.Registration(Email, Password, ConfirmPassword);

    result.subscribe((data) => {
      this.loading = false;
      this.router.navigate(['/']);
    }, (data) => {
      this.loading = false;
      // notify(data.error.message, 'error', 2000);
    });
  }

}
