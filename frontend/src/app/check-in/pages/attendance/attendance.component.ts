import { HttpErrorResponse, HttpHeaderResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CheckinService } from 'src/app/shared/checkin/checkin.service';
import { Attendance } from 'src/app/shared/model/Attendance';
import { MessageService } from 'primeng/api';
import { LeaveCount, LeaveForm } from 'src/app/shared/model/LeaveData';
import { UserService } from 'src/app/shared/userServiceService/user.service';

@Component({
  selector: 'app-attendance',
  templateUrl: './attendance.component.html',
  styleUrls: ['./attendance.component.scss'],
  providers: [MessageService],
})
export class AttendanceComponent {
  checkDate:FormGroup;
  currData:Attendance;
  formatedDate:string
  userLeaveData:LeaveCount;
  maxDateCheckIn:Date;
  minDateLeave:Date;
  leaveForm:boolean=false;
  submitted: boolean = false;

  leaveType=[
    { label: 'CASUAL', value: 'CASUAL' },
    { label: 'SICK', value: 'SICK' },
    { label: 'EARNED', value: 'EARNED' }
];

  leaveFormData:LeaveForm;

  constructor(private fb: FormBuilder, private checkinService: CheckinService, private messageService: MessageService, public userService:UserService) {}

  ngOnInit(): void {
    this.initializeForm();
    console.log(this.currData);
    this.leaveData();

    // this.maxDateCheckIn=;
    this.maxDateCheckIn=new Date();

    this.minDateLeave=new Date();
    
  }

  initializeForm(): void {
    this.checkDate = this.fb.group({
      checkDate: this.fb.control(''),
      
    });
    this.valueChanges();
  }
  valueChanges(): void {
    this.checkDate.valueChanges.subscribe((newData) => {
      console.log(typeof(newData.checkDate));
      this.formatedDate=this.convertToDate(newData.checkDate);
      this.getDateData(this.formatedDate);
      // newData.checkDate = this.convertToDate(newData.checkDate)
    });
  }

  getDateData(newData){
    this.checkinService.getDateData(newData).subscribe({
      next:(response)=>{
        console.log(response);
        this.currData=response;
      },
      error:(error:HttpErrorResponse)=>{
        console.log(error);
      }
    })
  }

  logAttendance(){
    this.checkinService.logCheckInTime(this.checkDate.value.checkDate,this.getCurrentTime()).subscribe({
      next:(response)=>{
        console.log(response);
        this.getDateData(this.checkDate.value.checkDate);
        this.messageService.add({
          severity: 'success',
          summary: 'Successful',
          detail: 'Check-in successful',
          life: 3000,
        });
      }
    })
  }

  logCheckOut(){
    this.checkinService.updateLogCheck(this.formatedDate, this.currData.checkInTime,this.getCurrentTime()).subscribe({
      next:()=>{
        this.getDateData(this.checkDate.value.checkDate);
        this.messageService.add({
          severity: 'success',
          summary: 'Successful',
          detail: 'Check-out successful',
          life: 3000,
        });
      },
      error:(error)=>{
        console.log(error);
        
      }
    })
    }


  modifyCheckIn(){
    this.checkinService.updateLogCheck(this.formatedDate, this.currData.checkInTime, this.currData.checkOutTime).subscribe({
      next:(response)=>{
        console.log(response);
        this.messageService.add({
          severity: 'success',
          summary: 'Successful',
          detail: 'Check data updated successfully',
          life: 3000,
        });
      },
      error:(error)=>{
        console.log(error);
        
      }
    })
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

  getCurrentTime(): string {
    const date = new Date();
  
    let hours: string | number = date.getHours();
    let minutes: string | number = date.getMinutes();
    let seconds: string | number = date.getSeconds();
  
    // Add leading zero if needed
    hours = (hours < 10) ? `0${hours}` : hours;
    minutes = (minutes < 10) ? `0${minutes}` : minutes;
    seconds = (seconds < 10) ? `0${seconds}` : seconds;
  
    return `${hours}:${minutes}:${seconds}`;
  }

  formatTime(time: Date): string {
    let hours: string | number = time.getHours();
    let minutes: string | number = time.getMinutes();
    let seconds: string | number = time.getSeconds();
  
    // Add leading zero if needed
    hours = (hours < 10) ? `0${hours}` : hours;
    minutes = (minutes < 10) ? `0${minutes}` : minutes;
    seconds = (seconds < 10) ? `0${seconds}` : seconds;
  
    return `${hours}:${minutes}:${seconds}`;
  }


  //leave
  leaveData(){
    this.checkinService.getLeaveDate().subscribe({
      next:(response)=>{
        this.userLeaveData=response;
        console.log(this.userLeaveData);
        
      },
      error:(error:HttpErrorResponse)=>{
        console.log(error);
        
      }
    })
  }

  openLeaveForm(){
    this.leaveFormData=new LeaveForm();
    this.leaveFormData.userName=this.userService.userName;
    this.leaveForm=true;
    this.submitted=false;
  }

  getSeverity(type:string):string{
    switch(type){
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
  
applyForLeave(){
  this.submitted = true;
  this.checkinService.createLeaveApplication(this.leaveFormData).subscribe({
    next:(response)=>{
      console.log(response);
      
    },
    error:(err)=>{
      console.log(err);
      
    }
  })
}

}