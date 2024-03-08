import { Injectable } from '@angular/core';
import { UserService } from '../userServiceService/user.service';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { jwtDecode } from 'jwt-decode';
import { JWTToken } from '../model/JWT';
import { Subject } from 'rxjs';
import { AthTableData, EmployeeDetails } from '../model/EmployeeDetails';
import { Audit } from '../model/Audit';
import { LeaveCount } from '../model/LeaveData';

@Injectable({
  providedIn: 'root'
})
export class DisplayEmployeeService {

  constructor(private userService:UserService, private http:HttpClient) { }

  allEmployee$: Subject<EmployeeDetails[]> = new Subject<EmployeeDetails[]>();
  auditDetails:Audit=new Audit();
  newLeaveEntry:LeaveCount=new LeaveCount();

  getAllEmployeeDetails(){
    this.http
      .get<EmployeeDetails[]>(
        'https://localhost:7125/api/Users/allUsers',
        {}
      )
      .subscribe({
        next: (response) => {
          console.log(response);
          this.allEmployee$.next(response);
        },
        error: (error: HttpErrorResponse) => {
          console.log(error);
          
        },
        complete: () => {
          
        },
      });
  }
  getEmpData(){
    return this.http.get<EmployeeDetails[]>('https://localhost:7125/api/Users/allUsers',
    {});
  }

  deleteAuthData( userName:string){
    this.http.delete<any>(`https://localhost:7125/api/AuthCred/deleteAuth/${userName}`, {}).subscribe({
      next:()=>{
        console.log('authDeleted');
        
      }
    })
  }

  deleteEmployee(userName:string){
    this.deleteAuthData(userName);
    return this.http.delete(`https://localhost:7125/api/Users/user/delete?userName=${userName}`);
  }

  formatDate(dateString: string): string {
    // Parse the string into a Date object
    const date = new Date(dateString);
    // Format the date into "yyyy-mm-dd" format
    const year = date.getFullYear();
    const month = (date.getMonth() + 1).toString().padStart(2, '0'); 
    const day = date.getDate().toString().padStart(2, '0');
    return `${year}-${month}-${day}`;
}

  createEmployee(employeeData:EmployeeDetails){
    employeeData.overTime=0;
    employeeData.excemptionAmt=0;
    employeeData.taxPercent=(employeeData.salary*12<250000?0:employeeData.salary*12<500000?5:employeeData.salary*12<1000000?20:30);
    employeeData.dob=this.formatDate(employeeData.dob);
    employeeData.nextPayDate=this.formatDate(employeeData.nextPayDate);
    return this.http.post<any>('https://localhost:7125/api/Users/add-user-details', employeeData);
  }

  createLeaveData(userName:string){
    this.newLeaveEntry.userName=userName;
    this.newLeaveEntry.casualLeave=10;
    this.newLeaveEntry.earnedLeave=10.
    this.newLeaveEntry.sickLeave=10;
     this.http.post<any>('https://localhost:7125/api/Leave/addLeave',this.newLeaveEntry).subscribe({
      next:(response)=>{
        console.log('leave created');
        
      }
     })
  }

  createAuthNewEmp(employeeAuth:AthTableData){
  console.log(employeeAuth);
    return this.http.post<any>('https://localhost:7125/api/AuthCred/createUser', employeeAuth);
  }

  logAudit(empDetails:EmployeeDetails, operation:number, result:number){
    this.auditDetails.operatedOn=empDetails.userName;
    this.auditDetails.excecutedAt=this.formatCurrentDateTime();
    this.auditDetails.userName=this.userService.userName;
    this.auditDetails.result=(result==1?'SUCCESS':'FAIL');
    this.auditDetails.operation=(operation==1?'CREATED':operation===2?'EDIT':'DELETE');

    console.log(empDetails);
    
    this.http.post<Audit>(`https://localhost:7125/api/Audit/createAudit`,this.auditDetails).subscribe({
      next:()=>{
        console.log('Audit logged');
        
      },
      error:(error:HttpErrorResponse)=>{
        console.log(error);
        
      }
    })
  }

  editExistingUser(newData:EmployeeDetails){
    newData.dob=this.formatDate(newData.dob);
    newData.nextPayDate=this.formatDate(newData.nextPayDate);
    console.log(EmployeeDetails);
    return this.http.put<any>('https://localhost:7125/api/Users/userDetails/edit', newData);
  }

  padTo2Digits(num) {
    return num.toString().padStart(2, '0');
   }
   
  formatCurrentDateTime():string {
    const now = new Date();
    const year = now.getFullYear();
    const month = this.padTo2Digits(now.getMonth() + 1);
    const day = this.padTo2Digits(now.getDate());
    const hours = this.padTo2Digits(now.getHours());
    const minutes = this.padTo2Digits(now.getMinutes());
    const seconds = this.padTo2Digits(now.getSeconds());
   
    return `${year}-${month}-${day} ${hours}:${minutes}:${seconds}`;
   }



}
