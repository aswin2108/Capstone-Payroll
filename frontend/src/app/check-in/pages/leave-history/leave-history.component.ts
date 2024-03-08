import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { LeaveHistoryService } from 'src/app/shared/LeaveHistory/leave-history.service';
import { LeaveFormAPI } from 'src/app/shared/model/LeaveData';

@Component({
  selector: 'app-leave-history',
  templateUrl: './leave-history.component.html',
  styleUrls: ['./leave-history.component.scss']
})
export class LeaveHistoryComponent implements OnInit {
  constructor( private leaveHistoryService:LeaveHistoryService){}


  allLeaveApplications:LeaveFormAPI[];

  ngOnInit(): void {
      this.fetchLeaveHistory();
  }

  fetchLeaveHistory(){
    this.leaveHistoryService.getLeaveHistory().subscribe({
      next:(response:LeaveFormAPI[])=>{
        console.log(response);
        this.allLeaveApplications=response;
      },
      error:(error:HttpErrorResponse)=>{
        console.log(error);
        
      }
    });
  }

  getSeverity(status: number) {
    switch (status) {
        case 1:
            return 'success';
        case 0:
            return 'warning';
        case 2:
            return 'danger';
        default:
          return '';
    }
}

}
