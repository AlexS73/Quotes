import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {AuthService} from "../../Shared/Services/auth.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent implements OnInit {

  loginForm : FormGroup
  loading = false;

  constructor(private authService: AuthService, private router: Router) {
  }

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      Email : new FormControl('', [
        Validators.email,
        Validators.required
      ]),
      Password : new FormControl('', [
        Validators.required,
        Validators.minLength(8)
      ])
    });
  }

  Submit(e) {
    e.preventDefault();
    const { Email, Password } = this.loginForm.value;
    this.loading = true;

    const result = this.authService.LogIn(Email, Password);

    result.subscribe((data) => {
      this.loading = false;
      this.router.navigate(['/']);
    }, (data) => {
      this.loading = false;
      // notify(data.error.message, 'error', 2000);
    });
  }
}
