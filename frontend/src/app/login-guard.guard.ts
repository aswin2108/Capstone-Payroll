import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateFn, Router, RouterStateSnapshot, UrlTree, mapToCanActivate } from '@angular/router';
import { Observable } from 'rxjs';


@Injectable({
  providedIn : 'root'
})
export class loginGuardGuard implements CanActivate {
  constructor(private router:Router){}
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
   


    if(!localStorage.getItem('jwtToken')){
      this.router.navigate(['']);
      return true;
    }
    return true;
  }
  
  
  }

