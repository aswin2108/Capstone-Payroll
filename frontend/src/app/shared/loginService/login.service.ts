import { Injectable } from '@angular/core';
import { AuthLogin } from '../model/Auth';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { UserService } from '../userServiceService/user.service';
import { jwtDecode } from 'jwt-decode';
import { JWTToken } from '../model/JWT';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  constructor(private http: HttpClient, private userService: UserService,private router: Router) {}
   loginStatus:boolean=false;
  loginAuthentication(loginFormData: AuthLogin) {
    return this.http
      .post<any>(
        'https://localhost:7125/api/AuthCred/tryLogging',
        loginFormData
      );  
  }

  
}
