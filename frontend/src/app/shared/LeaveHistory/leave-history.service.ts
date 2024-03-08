import { Injectable } from '@angular/core';
import { UserService } from '../userServiceService/user.service';
import { HttpClient } from '@angular/common/http';
import { LeaveFormAPI } from '../model/LeaveData';

@Injectable({
  providedIn: 'root'
})
export class LeaveHistoryService {

  constructor(private userService:UserService, private http:HttpClient) { }

  getLeaveHistory(){
    return this.http.get<LeaveFormAPI[]>(`https://localhost:7125/api/LeaveRecords/approvedLeaves/${this.userService.userName}`);
  }

 

}
