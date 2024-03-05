import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserService } from '../userServiceService/user.service';
import { Attendance } from '../model/Attendance';
import { FormGroup, FormControl } from '@angular/forms';
import { LeaveCount, LeaveForm } from '../model/LeaveData';

@Injectable({
  providedIn: 'root',
})
export class CheckinService {
  constructor(private userService: UserService, private http: HttpClient) {
    this.initializeForm();
  }

  newLogData: Attendance = new Attendance();
  attendanceForm: FormGroup;

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
    const month: number = date.getMonth() + 1; // January is 0, February is 1, etc.
    const day: number = date.getDate();

    // Pad single digit days and months with a leading zero
    const formattedMonth: string = month < 10 ? `0${month}` : `${month}`;
    const formattedDay: string = day < 10 ? `0${day}` : `${day}`;

    return `${year}-${formattedMonth}-${formattedDay}`;
  }

  formatTime(time: Date): string {
    let hours: string | number = time.getHours();
    let minutes: string | number = time.getMinutes();
    let seconds: string | number = time.getSeconds();

    // Add leading zero if needed
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

  createLeaveApplication(leaveData:LeaveForm){

    return this.http.post<any>('https://localhost:7125/api/LeaveRecords/leaveSubmission', leaveData);
  }
}
