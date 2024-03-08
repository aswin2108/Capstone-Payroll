import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ManageEmplyeeComponent } from './manage-emplyee.component';
import { DisplayEmployeeComponent } from './pages/display-employee/display-employee.component';
import { loginGuardGuard } from '../login-guard.guard';



const routes: Routes = [
  
  { path: '', component: ManageEmplyeeComponent ,  canActivate: [loginGuardGuard],
    children:[{

      path: '', component: DisplayEmployeeComponent,   canActivate: [loginGuardGuard] 
    }]
  },
  ];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ManageEmplyeeRoutingModule {}
