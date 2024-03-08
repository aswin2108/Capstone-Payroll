import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SalaryComponent } from './salary.component';
import { SalarySlipComponent } from './pages/salary-slip/salary-slip.component';
import { loginGuardGuard } from '../login-guard.guard';

const routes: Routes = [
  {
    path: '',
    component: SalaryComponent,    canActivate: [loginGuardGuard]
  },
  {
    path:'print',
    component: SalarySlipComponent,    canActivate: [loginGuardGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class SalaryRoutingModule {}
