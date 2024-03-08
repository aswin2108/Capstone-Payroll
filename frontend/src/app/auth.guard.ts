// import { CanActivateFn } from '@angular/router';

// export const authGuard: CanActivateFn = (route, state) => {
//   return true;
// };

import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router, Route, UrlSegment, CanLoad } from '@angular/router';
import { Observable } from 'rxjs';
import { UserService } from './shared/userServiceService/user.service';
// Import your authentication service (if applicable)

@Injectable({
  providedIn: 'root'
})
export class AuthGuard  {
  constructor(private router: Router, private userService:UserService){}

  // canActivate(
  //   route: ActivatedRouteSnapshot,
  //   state: RouterStateSnapshot):
  //   //Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

  //   // Check for authenticated user (replace with your authentication logic)
  //   // if ( !this.userService.isTokenExpired() ) {
  //   //   return true;
  //   // }

  //   // if (state.url !== '/home') {
  //   //   this.router.navigate(['/authentication']); // Redirect to login on unauthorized access
  //   // }

  //   // // this.router.navigate(['/authentication']); // Redirect to login on unauthorized access
  //   // return false;
  // }

  
}


// export class AuthenticationGuard implements CanLoad {
//   constructor(private router: Router, private userService: UserService) {}

//   // canLoad(route:Route,segments:UrlSegment[]):Observable<boolean>|Promise<boolean>|boolean{
//   //   if(!this.userService.isTokenExpired()){
//   //     return true;
//   //   }
//   //   this.router.navigate(['/authentication']);
//   //   return false;
//   // }
// }

