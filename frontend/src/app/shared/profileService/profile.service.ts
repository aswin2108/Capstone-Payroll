import { Injectable } from '@angular/core';
import { UserService } from '../userServiceService/user.service';
import { HttpClient } from '@angular/common/http';
import { EmployeeDetails } from '../model/EmployeeDetails';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  userName:string
  constructor(private userService:UserService, private http: HttpClient) {
    this.userName=userService.userName
  }

  fetchDashboardData(){
    return this.http.get<any>(`https://localhost:7125/api/Users/UserName/${this.userName}`, {})
  }

  updateExcemptionAndOverTime(newProfileData:EmployeeDetails){
    return this.http.put<any>('https://localhost:7125/api/Users/UserName/newExcemptionAmt/newTaxPercent',newProfileData);
  }
}
