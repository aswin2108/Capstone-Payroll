import { Component, OnInit } from '@angular/core';
import { DisplayEmployeeService } from 'src/app/shared/displayEmployee/display-employee.service';
import {
  AthTableData,
  EmployeeDetails,
} from 'src/app/shared/model/EmployeeDetails';
import { ConfirmationService, MessageService } from 'primeng/api';
import { HttpErrorResponse } from '@angular/common/http';
import * as cloneDeep from 'lodash/cloneDeep';
import jsPDF from 'jspdf';
import 'jspdf-autotable';
import * as XLSX from 'xlsx';
import { saveAs } from 'file-saver';
import * as FileSaver from 'file-saver';
import { UserService } from 'src/app/shared/userServiceService/user.service';

@Component({
  selector: 'app-display-employee',
  templateUrl: './display-employee.component.html',
  styleUrls: ['./display-employee.component.scss'],
  providers: [ConfirmationService, MessageService],
})
export class DisplayEmployeeComponent implements OnInit {
  constructor(
    private displayEmplyeeService: DisplayEmployeeService,
    private confirmationService: ConfirmationService,
    private messageService: MessageService, public userService:UserService
  ) {
    this.getEmployeeData();
  }

  allEmployees: EmployeeDetails[];
  newEmployAuth = new AthTableData();
  postORput: number = 1;
  selectedEmployes;
  employee: EmployeeDetails;
  employeeDialog: boolean = false;
  newEmployee: EmployeeDetails;
  submitted: boolean = false;
  rolesAvailable = [
    { label: 'ADMIN', value: 'ADMIN' },
    { label: 'HR', value: 'HR' },
    { label: 'EMPLOYEE', value: 'EMPLOYEE' },
  ];

  ngOnInit(): void {
    this.fetchAllEmployees();
  }

  openNew() {
    this.newEmployee = new EmployeeDetails();
    this.newEmployAuth = new AthTableData();
    this.submitted = false;
    this.postORput = 2;
    this.employeeDialog = true;
  }

  editEmployee(employeeData: EmployeeDetails) {
    this.newEmployee = { ...employeeData };
    this.postORput = 3;
    this.employeeDialog = true;
  }

  hideDialog() {
    this.employeeDialog = false;
    this.submitted = false;
  }

  deleteEmployee(employeeData: EmployeeDetails) {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete ' + employeeData.userName + '?',
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.displayEmplyeeService
          .deleteEmployee(employeeData.userName)
          .subscribe({
            next: () => {
              console.log('haii');
              
              this.displayEmplyeeService.logAudit(employeeData,3,1);
              employeeData = null;
              this.fetchAllEmployees();
              this.messageService.add({
                severity: 'success',
                summary: 'Successful',
                detail: 'Employee data Deleted',
                life: 3000,
              });
            },
            error: (error) => {
              this.displayEmplyeeService.logAudit(this.newEmployee,3,0);
              console.log(error);
              this.messageService.add({
                severity: 'danger',
                summary: 'Unsuccessful',
                detail: 'Data Deleted Failed',
                life: 3000,
              });
            },
          });
      },
    });
  }

  getTagValue(roleValue: number): string {
    if (roleValue === 1) {
      return 'HR';
    } else if (roleValue === 2) {
      return 'ADMIN';
    } else {
      return 'EMPLOYEE';
    }
  }

  exportExcel() {
    import('xlsx').then((xlsx) => {
      const worksheet = xlsx.utils.json_to_sheet(this.allEmployees);
      const workbook = { Sheets: { data: worksheet }, SheetNames: ['data'] };
      const excelBuffer: any = xlsx.write(workbook, {
        bookType: 'xlsx',
        type: 'array',
      });
      this.saveAsExcelFile(excelBuffer, 'employeeData');
    });
  }
  saveAsExcelFile(buffer: any, fileName: string): void {
    let EXCEL_TYPE =
      'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8';
    let EXCEL_EXTENSION = '.xlsx';
    const data: Blob = new Blob([buffer], {
      type: EXCEL_TYPE,
    });
    FileSaver.saveAs(
      data,
      fileName + '_export_' + new Date().getTime() + EXCEL_EXTENSION
    );
  }

  deleteSelectedData() {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete the selected employee data?',
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.selectedEmployes.forEach((emp) => {
          this.displayEmplyeeService.deleteEmployee(emp.userName).subscribe({
            next: () => {
              this.displayEmplyeeService.logAudit(emp,3,1);
              this.selectedEmployes = null;
              this.fetchAllEmployees();
              this.messageService.add({
                severity: 'success',
                summary: 'Successful',
                detail: 'Employee Deleted',
                life: 3000,
              });
            },
            error: (error: HttpErrorResponse) => {
              this.displayEmplyeeService.logAudit(emp,3,0);
              console.log(error);
              this.messageService.add({
                severity: 'danger',
                summary: 'Unsuccessful',
                detail: 'Data Deletion Failed',
                life: 3000,
              });
            },
          });
        });
      },
    });
  }

  getSeverity(status: string) {
    switch (status) {
      case 'ADMIN':
        return 'success';
      case 'HR':
        return 'warning';
      case 'EMPLOYEE':
        return 'secondary';
      default:
        return '';
    }
  }

  getEmployeeData() {
    this.displayEmplyeeService.allEmployee$.subscribe((data) => {
      console.log(data);
      this.allEmployees = cloneDeep(data);
    });
  }

  fetchAllEmployees() {
    this.displayEmplyeeService.getAllEmployeeDetails();
  }

  createEmployee() {
    this.submitted = true;
    this.newEmployAuth.role =
    this.newEmployee.role === 'Admin'
    ? 2
    : this.newEmployee.role === 'HR'
    ? 1
    : 0;
    
    if(this.newEmployee.role.toUpperCase()==='ADMIN'){
      this.newEmployee.userName='A'+this.newEmployee.userName.toLowerCase();
    }
    else if(this.newEmployee.role.toUpperCase()==='HR'){
      this.newEmployee.userName='H'+this.newEmployee.userName.toLowerCase();
    }
    else{
      this.newEmployee.userName='E'+this.newEmployee.userName.toLowerCase();
    }
    this.newEmployAuth.userName = this.newEmployee.userName;
    this.displayEmplyeeService.createAuthNewEmp(this.newEmployAuth).subscribe({
      next: (response) => {
        console.log(response);
      },
      error: (error: HttpErrorResponse) => {
        console.log(error);
        if(error.error.text==='User Exist'){
          this.displayEmplyeeService.logAudit(this.newEmployee,1,0);
          this.messageService.add({
            severity: 'info',
            summary: 'Unsuccessfull',
            detail: 'UserName already exists',
            life: 3000,
          });
        }
        else if (error.status === 200)
          this.displayEmplyeeService
            .createEmployee(this.newEmployee)
            .subscribe({
              next: (response) => {
                // this.fetchAllEmployees();
                // this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Employee Deleted', life: 3000 });
              },
              error: (error: HttpErrorResponse) => {
                this.displayEmplyeeService.logAudit(this.newEmployee,1,1);
                console.log(error);
                if (error.status === 200){ 
                  this.fetchAllEmployees();
                  this.displayEmplyeeService.createLeaveData(this.newEmployee.userName);
                  this.messageService.add({
                    severity: 'success',
                    summary: 'Successful',
                    detail: 'Employee Added',
                    life: 3000,
                  });
                }
                this.hideDialog();
              },
            });
      },
    });
  }

  saveEditEmployee() {
    this.submitted = true;
    console.log();
    
    this.displayEmplyeeService.editExistingUser(this.newEmployee).subscribe({
      next: (response) => {
        this.displayEmplyeeService.logAudit(this.newEmployee,2,1);
        this.fetchAllEmployees();
        this.messageService.add({
          severity: 'success',
          summary: 'Successful',
          detail: 'Employee Details Modified',
          life: 3000,
        });
      },
      error: (error: HttpErrorResponse) => {
        this.displayEmplyeeService.logAudit(this.newEmployee,2,0);
        console.log(error);
      },
      complete:()=>{
        this.hideDialog();
      }
    });
    this.fetchAllEmployees();
  }

  test(){
    console.log("haiii");
    
  }

//   handleFileUpload(event: any) {
//     console.log('csv process started');
//     const fileReader = new FileReader();
//     fileReader.onload = (e) => {
//         const fileContents = fileReader.result.toString();
//         this.processCsvData(fileContents);
//     };
//     fileReader.readAsText(event.files[0]);
// }

handleFileUpload(event: Event) {
  const files: FileList = (event.target as HTMLInputElement).files;
  if (files && files.length > 0) {
    const file: File = files[0];
    const fileReader: FileReader = new FileReader();
    fileReader.onload = (e) => {
      const fileContents = fileReader.result as string;
      this.processCsvData(fileContents);
    };
    fileReader.readAsText(file);
  }
}
processCsvData(csvData: string) {
  const rows = csvData.split('\n');
  const employeeArray: EmployeeDetails[] = [];
  
  console.log('csv process started');
  

  for (let i = 1; i < rows.length; i++) {
      const values = rows[i].split(',');
      console.log(values);
      
      if (values.length === 18) {
          const employee: EmployeeDetails = new EmployeeDetails();
          const employAuth: AthTableData = new AthTableData();

          this.newEmployee=new EmployeeDetails();
          this.newEmployAuth=new AthTableData();
          employee.userName = values[0].trim();
          employee.age = +values[1].trim();
          employee.email = values[2].trim();
          employee.accNo = values[3].trim();
          employee.firstName = values[4].trim();
          employee.lastName = values[5].trim();
          employee.ifsc = values[6].trim();
          employee.nextPayDate = values[7].trim();
          employee.dob = values[8].trim();
          employee.phone = values[9].trim();
          employee.salary = +values[10].trim();
          employee.taxPercent = +values[11].trim();
          employee.bonus = +values[12].trim();
          employee.excemptionAmt = +values[13].trim();
          employee.payFreq = +values[14].trim();
          employee.overTime = +values[15].trim();
          employee.role = values[16].trim();

          employAuth.password=values[17].trim();
          employAuth.userName = employee.userName;
          employeeArray.push(employee);

          // Call the post request function for each employee 
          
          this.newEmployee=employee;

          this.createEmployeeByValue(employee,employAuth);
      } else {
          console.log("Invalid row data");
      }
  }

  console.log(employeeArray); // Use this array for further processing if needed
}

   
createEmployeeByValue(employee, employeeAuth) {
    this.submitted = true;
    employeeAuth.role =
    employee.role === 'Admin'
    ? 2
    : employee.role === 'HR'
    ? 1
    : 0;
    
    if(employee.role.toUpperCase()==='ADMIN'){
      employee.userName='A'+employee.userName.toLowerCase();
    }
    else if(employee.role.toUpperCase()==='HR'){
      employee.userName='H'+employee.userName.toLowerCase();
    }
    else{
      employee.userName='E'+employee.userName.toLowerCase();
    }
    employeeAuth.userName = employee.userName;
    this.displayEmplyeeService.createAuthNewEmp(employeeAuth).subscribe({
      next: (response) => {
        console.log(response);
      },
      error: (error: HttpErrorResponse) => {
        console.log(error);
        if(error.error.text==='User Exist'){
          this.displayEmplyeeService.logAudit(this.newEmployee,1,0);
          this.messageService.add({
            severity: 'info',
            summary: 'Unsuccessfull',
            detail: 'UserName already exists',
            life: 3000,
          });
        }
        else if (error.status === 200)
          this.displayEmplyeeService
            .createEmployee(employee)
            .subscribe({
              next: (response) => {
                // this.fetchAllEmployees();
                // this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Employee Deleted', life: 3000 });
              },
              error: (error: HttpErrorResponse) => {
                this.displayEmplyeeService.logAudit(employee,1,1);
                console.log(error);
                if (error.status === 200){ 
                  this.fetchAllEmployees();
                  this.displayEmplyeeService.createLeaveData(employee.userName);
                  this.messageService.add({
                    severity: 'success',
                    summary: 'Successful',
                    detail: 'Employee Added',
                    life: 3000,
                  });
                }
                this.hideDialog();
              },
            });
      },
    });
  }
  
}
