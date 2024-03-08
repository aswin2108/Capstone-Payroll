// import { HttpClient } from '@angular/common/http';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Route, Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { Password } from 'primeng/password';
import { Subject } from 'rxjs';
import { LoginService } from 'src/app/shared/loginService/login.service';
import { JWTToken } from 'src/app/shared/model/JWT';
import { UserService } from 'src/app/shared/userServiceService/user.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm:FormGroup
  loading:boolean=false
  // userChange: Subject<any> = new Subject<any>();

  constructor(private fb:FormBuilder, private loginService:LoginService, private router: Router, private userService:UserService){}

  ngOnInit(): void {
      this.initializeLoginForm();
  }

  initializeLoginForm(){
    this.loginForm=this.fb.group({
      userName: this.fb.control(''),
      Password: this.fb.control(''),
    });
  }

  submitLogInForm(){
    this.loading=true;
    this.loginService.loginAuthentication(this.loginForm.value).subscribe({
      next: (response) => {
        const decoded: JWTToken = jwtDecode(response);
        this.userService.tokenDetails = decoded;
        this.userService.userName=decoded.unique_name;
        this.userService.role=decoded.role;
        this.userService.userChange.next([this.userService.userName, this.userService.role]);
        localStorage.setItem('jwtToken', response);
        this.router.navigate(['../../home']);
      },
      error: (error: HttpErrorResponse) => {
        if (error.status === 401) {
          console.log('Unauthorized');
          alert('userName or password is wrong');
        } else {
          console.error('An error occurred:', error.error.message);
        }
      },
      complete: () => {
        this.router.navigate(['/home']);
      },
    });
    
    this.loading=false;
  }
}
