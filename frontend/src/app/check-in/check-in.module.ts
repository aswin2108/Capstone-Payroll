import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CheckInRoutingModule } from './check-in-routing.module';
import { CheckInComponent } from './check-in.component';
import { AttendanceComponent } from './pages/attendance/attendance.component';
import { CalendarModule } from 'primeng/calendar';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastModule } from 'primeng/toast';
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { RadioButtonModule } from 'primeng/radiobutton';
import { TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { LeaveHistoryComponent } from './pages/leave-history/leave-history.component';


@NgModule({
  declarations: [
    CheckInComponent,
    AttendanceComponent,
    LeaveHistoryComponent,
  ],
  imports: [
    CommonModule,
    CheckInRoutingModule,
    CalendarModule,
    ReactiveFormsModule,
    FormsModule,
    ToastModule,
    DialogModule,
    TableModule,
    TagModule,
    DropdownModule,
    RadioButtonModule,
    InputNumberModule,
    ButtonModule,
    InputTextModule,
    InputTextareaModule 
  ]
})
export class CheckInModule { }
