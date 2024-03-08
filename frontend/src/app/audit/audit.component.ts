import { Component, OnInit } from '@angular/core';
import { UserOptions } from 'jspdf-autotable';
import { UserService } from '../shared/userServiceService/user.service';
import { AuditService } from '../shared/audit/audit.service';
import { HttpClient } from '@angular/common/http';
import { Audit } from '../shared/model/Audit';

@Component({
  selector: 'app-audit',
  templateUrl: './audit.component.html',
  styleUrls: ['./audit.component.scss']
})
export class AuditComponent implements OnInit{
  constructor(private auditService:AuditService){}

  ngOnInit(): void {
      this.getAllAudit();
  }

  allAuditEntries:Audit[];

  getAllAudit(){
    this.auditService.fetchAudit().subscribe({
      next:(response)=>{
        this.allAuditEntries=this.sortByDateDescending(response);
      },
      error:(error:HttpClient)=>{
        console.log(error);
      }
    });
  }

  getSeverityOperator(operation:string){
    switch(operation){
      case 'EDIT':
        return 'warning'
      case 'DELETE':
        return 'danger'
      case 'CREATED':
        return 'success'
      default:
        return '';
    }
  }
  getSeverityStatus(status:string){
    switch(status){
      case 'FAIL':
        return 'danger'
      case 'SUCCESS':
        return 'success'
      default:
        return '';
    }
  }
  
  sortByDateDescending(response:Audit[]) {
    return response.sort((a, b) => {
        const dateA = new Date(a.excecutedAt).getTime();;
        const dateB = new Date(b.excecutedAt).getTime();;
        return dateB - dateA;
    });
    
  }
}
