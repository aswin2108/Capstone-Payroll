import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ConfirmationService, MessageService } from 'primeng/api';
import { EmployeeDetails } from 'src/app/shared/model/EmployeeDetails';
import { ProfileService } from 'src/app/shared/profileService/profile.service';
import { UserService } from 'src/app/shared/userServiceService/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
  providers: [ConfirmationService, MessageService],
})
export class ProfileComponent implements OnInit {

  constructor(private profileService:ProfileService, private fb:FormBuilder,private formBuilder: FormBuilder, private messageService: MessageService){}

  userProfileData:EmployeeDetails;
  profileForm: FormGroup;

  ngOnInit(): void {
      this.fetchProfile();
      this.initializeForm();
      
  }

  newData = {
    excemptionAmt: '',
    overTime: ''
  };

  initializeForm(){
    this.profileForm = this.formBuilder.group({
      excemptionAmt: '',
      overTime: ''
    });
  }

  fetchProfile(){
    this.profileService.fetchDashboardData().subscribe({
      next:(response)=>{
        console.log(response);
        this.userProfileData=response;
        this.setFieldValue();
      },
      error:(error:HttpErrorResponse)=>{
        console.log(error);
        
      }
    })
  }

  updateFields(){
    const formData = this.profileForm.value;
    let newProfileData=this.userProfileData;
    newProfileData.overTime=formData.overTime;
    newProfileData.excemptionAmt=formData.excemptionAmt;
    this.profileService.updateExcemptionAndOverTime(newProfileData).subscribe({
      next:(response)=>{
        console.log(response);
        this.fetchProfile();
        this.messageService.add({
          severity: 'success',
          summary: 'Successful',
          detail: 'Data update',
          life: 3000,
        });
      },
      error:(error:HttpErrorResponse)=>{
        this.messageService.add({
          severity: 'success',
          summary: 'Successful',
          detail: 'Data update',
          life: 3000,
        });
      }
    })
    
  }

  setFieldValue() {
    this.profileForm.setValue({
      excemptionAmt: this.userProfileData.excemptionAmt,
      overTime: this.userProfileData.overTime
    });
  }

}
