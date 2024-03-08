import { HttpErrorResponse, HttpHeaderResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CheckinService } from 'src/app/shared/checkin/checkin.service';
import { Attendance } from 'src/app/shared/model/Attendance';
import { MessageService } from 'primeng/api';
import {
  LeaveApp,
  LeaveCount,
  LeaveForm,
  LeaveFormAPI,
} from 'src/app/shared/model/LeaveData';
import { UserService } from 'src/app/shared/userServiceService/user.service';

@Component({
  selector: 'app-attendance',
  templateUrl: './attendance.component.html',
  styleUrls: ['./attendance.component.scss'],
  providers: [MessageService],
})
export class AttendanceComponent {
  checkDate: FormGroup;
  currData: Attendance;
  formatedDate: string;
  userLeaveData: LeaveCount;
  maxDateCheckIn: Date;
  minDateLeave: Date;
  leaveForm: boolean = false;
  submitted: boolean = false;
  leaveApplicationType: string;
  allLeaveApplications:LeaveApp[];

  leaveType = [
    { label: 'CASUAL', value: 'CASUAL' },
    { label: 'SICK', value: 'SICK' },
    { label: 'EARNED', value: 'EARNED' },
  ];

  leaveFormDataApi: LeaveFormAPI = new LeaveFormAPI();
  leaveFormData: LeaveForm;
  minDate: Date;

  constructor(
    private fb: FormBuilder,
    private checkinService: CheckinService,
    private messageService: MessageService,
    public userService: UserService
  ) {}

  ngOnInit(): void {
    this.minDate = new Date();

    if (this.userService.role == 'Admin') this.leaveApplicationType = 'H';
    else if (this.userService.role == 'HR') this.leaveApplicationType = 'E';
    this.getLeaveApplication();
    this.initializeForm();
    this.leaveData();

    this.maxDateCheckIn = new Date();
    this.minDateLeave = new Date();
  }

  initializeForm(): void {
    this.checkDate = this.fb.group({
      checkDate: this.fb.control(''),
    });
    this.valueChanges();
  }
  valueChanges(): void {
    this.checkDate.valueChanges.subscribe((newData) => {
      this.formatedDate = this.convertToDate(newData.checkDate);
      this.getDateData(this.formatedDate);
    });
  }

  getDateData(newData) {
    this.checkinService.getDateData(newData).subscribe({
      next: (response) => {
        this.currData = response;
      },
      error: (error: HttpErrorResponse) => {
        console.log(error);
      },
    });
  }

  logAttendance() {
    this.checkinService
      .logCheckInTime(this.formatedDate, this.getCurrentTime())
      .subscribe({
        next: (response) => {
          this.getDateData(this.formatedDate);
          this.messageService.add({
            severity: 'success',
            summary: 'Successful',
            detail: 'Check-in successful',
            life: 3000,
          });
        },
      });
  }

  logCheckOut() {
    this.checkinService
      .updateLogCheck(
        this.formatedDate,
        this.currData.checkInTime,
        this.getCurrentTime()
      )
      .subscribe({
        next: () => {
          this.getDateData(this.formatedDate);
          this.messageService.add({
            severity: 'success',
            summary: 'Successful',
            detail: 'Check-out successful',
            life: 3000,
          });
        },
        error: (error) => {
          console.log(error);
        },
      });
  }

  modifyCheckIn() {
    this.checkinService
      .updateLogCheck(
        this.formatedDate,
        this.currData.checkInTime,
        this.currData.checkOutTime
      )
      .subscribe({
        next: (response) => {
          console.log(response);
          this.messageService.add({
            severity: 'success',
            summary: 'Successful',
            detail: 'Check data updated successfully',
            life: 3000,
          });
        },
        error: (error) => {
          console.log(error);
        },
      });
  }

  convertToDate(date: Date): string {
    const year: number = date.getFullYear();
    const month: number = date.getMonth() + 1; 
    const day: number = date.getDate();
    const formattedMonth: string = month < 10 ? `0${month}` : `${month}`;
    const formattedDay: string = day < 10 ? `0${day}` : `${day}`;

    return `${year}-${formattedMonth}-${formattedDay}`;
  }

  getCurrentTime(): string {
    const date = new Date();

    let hours: string | number = date.getHours();
    let minutes: string | number = date.getMinutes();
    let seconds: string | number = date.getSeconds();
    hours = hours < 10 ? `0${hours}` : hours;
    minutes = minutes < 10 ? `0${minutes}` : minutes;
    seconds = seconds < 10 ? `0${seconds}` : seconds;

    return `${hours}:${minutes}:${seconds}`;
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
  leaveData() {
    this.checkinService.getLeaveDate().subscribe({
      next: (response) => {
        this.userLeaveData = response;
      },
      error: (error: HttpErrorResponse) => {
        console.log(error);
      },
    });
  }

  openLeaveForm() {
    this.leaveFormData = new LeaveForm();
    this.leaveFormData.userName = this.userService.userName;
    this.leaveForm = true;
    this.submitted = false;
  }

  getSeverity(type: string): string {
    switch (type) {
      case 'CASUAL':
        return 'success';
      case 'SICK':
        return 'warning';
      case 'EARNED':
        return 'secondary';
      default:
        return '';
    }
  }

  hideDialog() {
    this.leaveForm = false;
    this.submitted = false;
  }

  applyForLeave() {
    this.submitted = true;
    this.leaveFormDataApi.leaveFrom = this.convertToDate(
      this.leaveFormData.leaveFrom
    );
    this.leaveFormDataApi.leaveTo = this.convertToDate(
      this.leaveFormData.leaveTo
    );
    this.leaveFormDataApi.userName = this.leaveFormData.userName;
    this.leaveFormDataApi.reason = this.leaveFormData.reason;
    this.leaveFormDataApi.flag = 0;
    this.leaveFormDataApi.leaveType =
      this.leaveFormData.leaveType === 'CASUAL'
        ? 1
        : this.leaveFormData.leaveType === 'SICK'
        ? 2
        : 3;
    this.checkinService
      .createLeaveApplication(this.leaveFormDataApi)
      .subscribe({
        next: (response) => {
          this.hideDialog();
          console.log(response);
          this.messageService.add({
            severity: 'success',
            summary: 'Successful',
            detail: 'Leave Application Filed',
            life: 3000,
          });
        },
        error: (err) => {
          console.log(err);
        },
      });
  }

  getLeaveApplication(){
    this.checkinService.applicationByRole(this.leaveApplicationType).subscribe({
      next:(response)=>{
        console.log(response);
        this.allLeaveApplications=response;
      },
      error:(error:HttpErrorResponse)=>{
        console.log(error);
        
      }
    });
  }

  approveLeave(application: LeaveApp){
    this.checkinService.updateLeaveCount(application).subscribe({
      next:()=>{
        this.checkinService.updateApplicationStatus(application,1).subscribe({
          next:()=>{
            this.messageService.add({
              severity: 'info',
              summary: 'Successful',
              detail: 'Leave Application Approved',
              life: 3000,
            });
            this.getLeaveApplication();
          },
          error:(error:HttpErrorResponse)=>{
            console.log(error);
            
          }
        })
       
      },
      error:(error:HttpErrorResponse)=>{
        console.log(error);
        this.messageService.add({
          severity: 'danger',
          summary: 'Successful',
          detail: 'Operation failed',
          life: 3000,
        });
      }
    })
  }

  rejectApplication(application:LeaveApp){
    this.checkinService.updateApplicationStatus(application,2).subscribe({
      next:()=>{
        this.messageService.add({
          severity: 'info',
          summary: 'Successful',
          detail: 'Leave Application rejected',
          life: 3000,
        });
        this.getLeaveApplication();
      },
      error:(error:HttpErrorResponse)=>{
        console.log(error);
        this.messageService.add({
          severity: 'danger',
          summary: 'Successful',
          detail: 'Operation failed',
          life: 3000,
        });
      }
    })
  }

}
