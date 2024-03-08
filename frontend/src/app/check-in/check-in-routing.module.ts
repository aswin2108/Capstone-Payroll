import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CheckInComponent } from './check-in.component';
import { AttendanceComponent } from './pages/attendance/attendance.component';
import { LeaveHistoryComponent } from './pages/leave-history/leave-history.component';
import { loginGuardGuard } from '../login-guard.guard';

const routes: Routes = [
  { path: '', component: CheckInComponent,  canActivate: [loginGuardGuard],
children:[
  {
    path:'',
    component:AttendanceComponent,    canActivate: [loginGuardGuard]
  },
  {
    path:'checkin/leaveHistory',
    component:LeaveHistoryComponent,    canActivate: [loginGuardGuard]
  }

]},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CheckInRoutingModule { }
