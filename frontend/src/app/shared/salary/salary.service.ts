import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserService } from '../userServiceService/user.service';
import { Salary } from '../model/Salary';
import { BehaviorSubject, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SalaryService {

  constructor(private userService:UserService, private http:HttpClient){}

  salaryData:Salary;

  getSalaryDetails(){
    return this.http.get<any>(
      `https://localhost:7125/api/SalaryCreditControler/creditDetails/${this.userService.userName}`, {}
    )
  }

  openSalarySlip(rowData:Salary){
    this.salaryData=rowData;
    console.log(rowData);
  }

  transferData(){
    return this.salaryData;
  }

}
