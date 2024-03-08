import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserService } from '../userServiceService/user.service';
import { Attendance } from '../model/Attendance';
import { FormGroup, FormControl } from '@angular/forms';
import { LeaveApp, LeaveCount, LeaveForm, LeaveFormAPI } from '../model/LeaveData';

@Injectable({
  providedIn: 'root',
})
export class CheckinService {
  constructor(private userService: UserService, private http: HttpClient) {
    this.initializeForm();
  }

  newLogData: Attendance = new Attendance();
  attendanceForm: FormGroup;
  leaveCntToReduce:LeaveCount=new LeaveCount();
  differenceInDays:number;

  initializeForm() {
    const formControls = {
      userName: new FormControl(''),
      date: new FormControl(''),
      checkInTime: new FormControl(''),
      checkOutTime: new FormControl(''),
    };
    this.attendanceForm = new FormGroup(formControls);
  }

  convertToDate(date: Date): string {
    const year: number = date.getFullYear();
    const month: number = date.getMonth() + 1; 
    const day: number = date.getDate();

    const formattedMonth: string = month < 10 ? `0${month}` : `${month}`;
    const formattedDay: string = day < 10 ? `0${day}` : `${day}`;

    return `${year}-${formattedMonth}-${formattedDay}`;
  }

  formatTime(time: Date): string {
    let hours: string | number = time.getHours();
    let minutes: string | number = time.getMinutes();
    let seconds: string | number = time.getSeconds();

    hours = hours < 10 ? `0${hours}` : hours;
    minutes = minutes < 10 ? `0${minutes}` : minutes;
    seconds = seconds < 10 ? `0${seconds}` : seconds;

    return `${hours}:${minutes}:${seconds}`;
  }

  getDateData(newDate: string) {
    console.log(typeof newDate);
    return this.http.get<any>(
      `https://localhost:7125/api/EmployeeLog/${this.userService.userName}/${newDate}`,
      {}
    );
  }

  logCheckInTime(date, time) {
    return this.http.post<any>(
      `https://localhost:7125/api/EmployeeLog/${this.userService.userName}/${date}/${time}`,
      {}
    );
  }

  updateLogCheck(date, inTime, outTime) {
    if (typeof inTime === 'string')
      this.attendanceForm.get('checkInTime').setValue(inTime);
    else
      this.attendanceForm.get('checkInTime').setValue(this.formatTime(inTime));
    if (typeof outTime === 'string')
      this.attendanceForm.get('checkOutTime').setValue(outTime);
    else
      this.attendanceForm
        .get('checkOutTime')
        .setValue(this.formatTime(outTime));
    this.attendanceForm.get('date').setValue(date);
    this.attendanceForm.get('userName').setValue(this.userService.userName);
    const data = this.attendanceForm.value;
    console.log(data);

    return this.http.put<void>(
      'https://localhost:7125/api/EmployeeLog/updateCheckTime',
      this.attendanceForm.value
    );
  }



  //leaves
  getLeaveDate(){
    return this.http.get<LeaveCount>(`https://localhost:7125/api/Leave/${this.userService.userName}`)
  }

  createLeaveApplication(leaveData:LeaveFormAPI){
    console.log(leaveData);
    
    return this.http.post<any>('https://localhost:7125/api/LeaveRecords/leaveSubmission', leaveData);
  }

  applicationByRole(leaveApplicationType:string){
    return this.http.get<LeaveApp[]>(`https://localhost:7125/api/LeaveRecords/unapprovedLeave/${leaveApplicationType}`,{});
  }

  //0-unapproved
  //1-approved
  //2-rejected
  updateApplicationStatus(application:LeaveApp, flag){
    return this.http.put<any>(`https://localhost:7125/api/LeaveRecords/approveLeave/${application.userName}/${application.leaveFrom}/${flag}`,{});
  }

  updateLeaveCount(application:LeaveApp){
    console.log(application);
    
    this.leaveCntToReduce.userName=application.userName;
    this.leaveCntToReduce.casualLeave=0;
    this.leaveCntToReduce.earnedLeave=0;
    this.leaveCntToReduce.sickLeave=0;

    const dateObjStart = new Date(application.leaveFrom);
    const dateObjEnd = new Date(application.leaveTo);
   
    const diffInMs = Math.abs(dateObjEnd.getTime() - dateObjStart.getTime());
    const diffInDays = diffInMs / (1000 * 60 * 60 * 24);

    if(application.leaveType===1)this.leaveCntToReduce.casualLeave=Math.floor(diffInDays);
    else if(application.leaveType===2)this.leaveCntToReduce.sickLeave=Math.floor(diffInDays);
    else this.leaveCntToReduce.earnedLeave=Math.floor(diffInDays);
   

    return this.http.put<any>('https://localhost:7125/api/Leave/updateLeave/UserName',this.leaveCntToReduce);
  }
}
