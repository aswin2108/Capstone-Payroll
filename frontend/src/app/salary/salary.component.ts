import { Component, Inject, OnInit } from '@angular/core';
import { SalaryService } from '../shared/salary/salary.service';
import { Salary } from '../shared/model/Salary';
import { HttpStatusCode } from '@angular/common/http';
import { UserService } from '../shared/userServiceService/user.service';
import * as FileSaver from 'file-saver';
import { DOCUMENT, DatePipe } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-salary',
  templateUrl: './salary.component.html',
  styleUrls: ['./salary.component.scss'],
  providers: [DatePipe]
})
export class SalaryComponent implements OnInit {
  // datePipe: any;
  constructor(private salaryService: SalaryService,private router: Router) { }

  allSalaryHistory:Salary[];

  cols = [
    { field: 'transactionId', header: 'Transaction ID'},
    { field: 'creditDate', header: 'Credit Date' },
    { field: 'taxCut', header: 'taxCut' },
    { field: 'creditAmount', header: 'Credit Amount' },
    { field: '', header: 'Action Button' },
];

  ngOnInit(): void {
      this.salaryService.getSalaryDetails().subscribe({
        next:(response)=>{
          this.allSalaryHistory=response;
          this.allSalaryHistory = response.map(salary => ({
            ...salary,
            creditDate: new Date(salary.creditDate) // Convert string to Date
          }));
          console.log(this.allSalaryHistory);
        },
        error:(error:HttpStatusCode)=>{
          console.log(error);
          
        }
      })
  }

  openSalarySlip(rowData){
    this.salaryService.openSalarySlip(rowData);
    this.router.navigate(['salary/print']);
  }

  exportExcel() {
    import('xlsx').then((xlsx) => {
        const worksheet = xlsx.utils.json_to_sheet(this.allSalaryHistory);
        const workbook = { Sheets: { data: worksheet }, SheetNames: ['data'] };
        const excelBuffer: any = xlsx.write(workbook, { bookType: 'xlsx', type: 'array' });
        this.saveAsExcelFile(excelBuffer, 'salaryDetails');
    });
}

saveAsExcelFile(buffer: any, fileName: string): void {
    let EXCEL_TYPE = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8';
    let EXCEL_EXTENSION = '.xlsx';
    const data: Blob = new Blob([buffer], {
        type: EXCEL_TYPE
    });
    FileSaver.saveAs(data, fileName + '_export_' + new Date().getTime() + EXCEL_EXTENSION);
}
}

