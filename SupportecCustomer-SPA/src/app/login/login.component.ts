import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  model: any = {};
  user: any = {};
  loginMode = true;
  registerMode = false;

  constructor(private authService: AuthService, private alertify: AlertifyService, private router: Router) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      this.alertify.success('Logged in successfully');
    }, error => {
      this.alertify.error(error);
    }, () => {
      this.router.navigate(['/products']);
    });
  }

  register() {
    if (this.model.password !== this.model.confirmPassword) {
      return false;
    }
    this.user = {
      email: this.model.email,
      password: this.model.password
    };
    this.authService.register(this.user).subscribe(() => {
      this.alertify.success('Registration successful');
      this.loginToggle();
    }, error => {
      this.alertify.error(error);
    });
  }

  loginToggle() {
    this.loginMode = true;
    this.registerMode = false;
  }

  registerToggle() {
    this.registerMode = true;
    this.loginMode = false;
  }

}
