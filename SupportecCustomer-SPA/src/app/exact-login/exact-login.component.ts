import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-exact-login',
  templateUrl: './exact-login.component.html',
  styleUrls: ['./exact-login.component.css']
})
export class ExactLoginComponent implements OnInit {
  baseUrl = 'http://localhost:5000/api/exact/';
  form: any = {};

  constructor(private http: HttpClient) { 
    this.exactLogin();
    console.log('wefwefwef')
  }

  ngOnInit() {
  }

  exactLogin() {
    this.http.get(this.baseUrl + 'token')
      .pipe(
        map((response: any) => {
          console.log(response);
          this.form = response;
        })
      );
  }

}
