import { Injectable } from '@angular/core';
import { JWTToken } from './../model/JWT';
import { jwtDecode } from 'jwt-decode';
import { Subject } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor() { 
    const token=localStorage.getItem('jwtToken');
    this.isTokenExpired(token);
  }
  loggedIn:boolean=false;
    tokenDetails : JWTToken;
    userName:string;
    role:string;
    userChange: Subject<string[]> = new Subject<string[]>();

    isTokenExpired(token){
      try{
        const decodedToken:JWTToken=jwtDecode(token);
        this.userName=decodedToken.unique_name;
        this.role=decodedToken.role;
        const currentTime=Math.floor(Date.now()/1000);
        return decodedToken.exp<currentTime;
      }
      catch(error){
        return true;
      }
    }

    logOut(){
      localStorage.removeItem('jwtToken');
      this.userName=undefined;
      this.role=undefined;
    }

}
